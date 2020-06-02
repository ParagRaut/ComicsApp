using System.Threading.Tasks;
using ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes.CalvinAndHobbesService;

namespace ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes
{
    public class CalvinAndHobbesComics : ICalvinAndHobbesComics
    {
        public async Task<string> CalvinAndHobbesComicUri()
        {
            var calvinAndHobbesServiceApi = new CalvinAndHobbesServiceApi();

            string comicStripUri = await calvinAndHobbesServiceApi.CalvinAndHobbesComicUrl();

            return comicStripUri;
        }
    }
}
