using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.CalvinAndHobbes;

public class CalvinAndHobbesService
{
    private readonly HttpClient _httpClient;

    public CalvinAndHobbesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetComicUri()
    {
        string source = await _httpClient.GetStringAsync("calvinandhobbes");

        string imageLink = GetImageUri(source);

        return imageLink;
    }

    private static string GetImageUri(string source)
    {
        var document = new HtmlDocument();

        document.LoadHtml(source);
        const string imageClassNode = "//a[contains(@class, 'js-item-comic-link')]/picture/img";

        HtmlNode imageNode = document.DocumentNode.SelectSingleNode(imageClassNode);

        string imageLink = imageNode.GetAttributeValue("src", "");

        return imageLink;
    }
}
