using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.Dilbert;

public class DilbertService
{
    private readonly HttpClient _httpClient;

    public DilbertService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetComicUri()
    {
        string dateRange = GetRandomDateRange();

        string source = await _httpClient.GetStringAsync(dateRange);

        string imageLink = GetImageUri(source);

        return imageLink;
    }

    private static string GetImageUri(string source)
    {
        var document = new HtmlDocument();

        document.LoadHtml(source);

        const string imageClassNode = "//img[contains(@class, 'img-comic')]";

        HtmlNodeCollection imageNode = document.DocumentNode.SelectNodes(imageClassNode);

        var imageLink = string.Empty;

        foreach (HtmlNode link in imageNode)
        {
            imageLink = link.GetAttributeValue("src", "");
        }

        imageLink = $"{imageLink}.png";

        return imageLink;
    }

    private static string GetRandomDateRange()
    {
        var random = new Random();
        var startDate = new DateTime(1989, 4, 16);
        int dateRange = (DateTime.Today - startDate).Days;
        return startDate.AddDays(random.Next(dateRange)).ToString("yyyy-MM-dd");
    }
}
