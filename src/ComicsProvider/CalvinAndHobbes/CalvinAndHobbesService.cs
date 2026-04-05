using System.Text.RegularExpressions;

namespace ComicsProvider.CalvinAndHobbes;

public partial class CalvinAndHobbesService
{
    private readonly HttpClient _httpClient;

    public CalvinAndHobbesService(HttpClient httpClient)
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
        var match = ContentUrlRegex().Match(source);

        if (!match.Success)
            throw new InvalidOperationException("Could not find comic image on the page. The website structure may have changed.");

        return match.Groups[1].Value;
    }

    [GeneratedRegex(@"""contentUrl""\s*:\s*""(https://featureassets\.gocomics\.com/assets/[a-f0-9]+)""")]
    private static partial Regex ContentUrlRegex();
}
