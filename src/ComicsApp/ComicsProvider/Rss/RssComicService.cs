using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ComicsProvider.Rss;

// Reads a webcomic RSS feed and returns a random comic image URL from the latest items.
public sealed partial class RssComicService
{
    private static readonly XNamespace ContentNamespace = "http://purl.org/rss/1.0/modules/content/";

    private readonly HttpClient _httpClient;

    public RssComicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRandomImageAsync(string feedUrl, CancellationToken cancellationToken = default)
    {
        var feed = await _httpClient.GetStringAsync(feedUrl, cancellationToken);
        var document = XDocument.Parse(feed);

        var imageUrls = document
            .Descendants("item")
            .Select(GetItemHtml)
            .Select(html => ImgSrcRegex().Match(html))
            .Where(match => match.Success)
            .Select(match => UpgradeToHttps(match.Groups[1].Value))
            .ToList();

        return imageUrls.Count == 0
            ? string.Empty
            : imageUrls[Random.Shared.Next(imageUrls.Count)];
    }

    // The comic markup lives in <description> for most feeds and in <content:encoded> for WordPress feeds.
    private static string GetItemHtml(XElement item)
    {
        var description = item.Element("description")?.Value ?? string.Empty;
        var encoded = item.Element(ContentNamespace + "encoded")?.Value ?? string.Empty;
        return description + encoded;
    }

    private static string UpgradeToHttps(string url) =>
        url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            ? string.Concat("https://", url.AsSpan("http://".Length))
            : url;

    [GeneratedRegex("<img\\b[^>]*?\\bsrc\\s*=\\s*[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase)]
    private static partial Regex ImgSrcRegex();
}
