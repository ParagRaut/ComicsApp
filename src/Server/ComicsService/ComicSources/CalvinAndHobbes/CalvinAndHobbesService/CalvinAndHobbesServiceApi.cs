using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes.CalvinAndHobbesService
{
    public class CalvinAndHobbesServiceApi
    {
        public async Task<string> CalvinAndHobbesComicUrl()
        {
            var baseUrl = new Uri($"https://www.gocomics.com/random/calvinandhobbes");

            var httpClient = new HttpClient();

            string source = await httpClient.GetStringAsync(baseUrl);

            string imageLink = this.GetUri(source);

            return imageLink;
        }

        private string GetUri(string source)
        {
            var document = new HtmlDocument();

            document.LoadHtml(source);
            const string imageClassNode = "//a[contains(@class, 'js-item-comic-link')]/picture/img";

            HtmlNode imageNode = document.DocumentNode.SelectSingleNode(imageClassNode);

            string imageLink = imageNode.GetAttributeValue("src", "");

            return imageLink;
        }
    }
}
