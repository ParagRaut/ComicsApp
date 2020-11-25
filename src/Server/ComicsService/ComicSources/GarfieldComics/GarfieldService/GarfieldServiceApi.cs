using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics.GarfieldService
{
    public class GarfieldServiceApi
    {
        public static async Task<string> GetGarfieldComicsUrl()
        {
            string dateRange = GetRandomDateRange();

            var baseUrl = new Uri($"https://www.gocomics.com/garfield/{dateRange}");

            var httpClient = new HttpClient();

            string source = await httpClient.GetStringAsync(baseUrl);

            string imageLink = GetUri(source);

            return imageLink;
        }

        private static string GetRandomDateRange()
        {
            var random = new Random();
            var startDate = new DateTime(1978, 6, 19);
            int dateRange = (DateTime.Today - startDate).Days;
            return startDate.AddDays(random.Next(dateRange)).ToString("yyyy/MM/dd");
        }

        private static string GetUri(string source)
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
}
