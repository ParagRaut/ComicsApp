using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.ComicSources.Garfield;

public static class GarfieldService
{
    public static async Task<string> GetComicUri()
    {
        string dateRange = GetRandomDateRange();

        var baseUrl = new Uri($"https://www.gocomics.com/garfield/{dateRange}");

        var httpClient = new HttpClient();

        string source = await httpClient.GetStringAsync(baseUrl);

        string imageLink = GetImageUri(source);

        return imageLink;
    }

    private static string GetRandomDateRange()
    {
        var random = new Random();
        var startDate = new DateTime(1978, 6, 19);
        int dateRange = (DateTime.Today - startDate).Days;
        return startDate.AddDays(random.Next(dateRange)).ToString("yyyy/MM/dd");
    }

    private static string GetImageUri(string source)
    {
        var document = new HtmlDocument();

        document.LoadHtml(source);

        const string imageClassNode = "//picture[@class='item-comic-image']";

        HtmlNodeCollection imageNode = document.DocumentNode.SelectNodes(imageClassNode);

        var imageLink = imageNode.Select(x => x.FirstChild.GetAttributeValue("src", "")).FirstOrDefault();

        imageLink = $"{imageLink}.png";

        return imageLink;
    }
}
