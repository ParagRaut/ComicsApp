using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.ComicSources.DilbertComics.DilbertService
{
    public class DilbertServiceApi
    {
        public async Task<string> GetDilbertComicsUrl()
        {
            var dateRange = GetRandomDateRange();

            var baseUrl = new Uri($"https://dilbert.com/strip/{dateRange}");

            var httpClient = new HttpClient();

            string source = await httpClient.GetStringAsync(baseUrl);

            var imageLink = GetUri(source);

            return imageLink;
        }

        private string GetUri(string source)
        {
            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(source);

            string imageClassNode = "//img[contains(@class, 'img-comic')]";

            var imageNode = document.DocumentNode.SelectNodes(imageClassNode);

            string imageLink = string.Empty;

            foreach (HtmlNode link in imageNode)
            {
                imageLink = link.GetAttributeValue("src", "");
            }
            return imageLink;
        }

        private string GetRandomDateRange()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1989, 4, 16);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range)).ToString("yyyy-MM-dd");
        }
    }
}
