using System.Text.Json;
using System.Text.RegularExpressions;

namespace ComicsProvider.Garfield;

public partial class GarfieldService
{
    private readonly HttpClient _httpClient;

    public GarfieldService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    protected internal async Task<string> GetComicUri()
    {
        string source = await this._httpClient.GetStringAsync("");

        string imageLink = GetImageUri(source);

        return imageLink;
    }

    private static string GetImageUri(string source)
    {
        var matches = ContentUrlRegex().Matches(source);

        if (matches.Count == 0)
            throw new InvalidOperationException("Could not find comic image on the page. The website structure may have changed.");

        // Pick a random comic from the ones available on the page
        var random = new Random();
        var match = matches[random.Next(matches.Count)];

        return match.Groups[1].Value;
    }

    [GeneratedRegex(@"""contentUrl""\s*:\s*""(https://featureassets\.gocomics\.com/assets/[a-f0-9]+)""")]
    private static partial Regex ContentUrlRegex();
}
