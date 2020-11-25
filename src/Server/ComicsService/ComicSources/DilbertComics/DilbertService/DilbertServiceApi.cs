using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.ComicSources.DilbertComics.DilbertService
{
    public class DilbertServiceApi
    {
        public static async Task<string> GetDilbertComicsUrl()
        {
            string dateRange = GetRandomDateRange();

            var baseUrl = new Uri($"https://dilbert.com/strip/{dateRange}");

            var httpClient = new HttpClient();

            string source = await httpClient.GetStringAsync(baseUrl);

            string imageLink = GetUri(source);

            return imageLink;
        }

        private static string GetUri(string source)
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
}
