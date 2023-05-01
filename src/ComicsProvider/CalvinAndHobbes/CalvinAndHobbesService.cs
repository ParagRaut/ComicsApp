using HtmlAgilityPack;

namespace ComicsProvider.CalvinAndHobbes;

public class CalvinAndHobbesService
{
    private readonly HttpClient _httpClient;

    public CalvinAndHobbesService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    protected internal async Task<string> GetComicUri()
    {
        string source = await this._httpClient.GetStringAsync("calvinandhobbes");

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
