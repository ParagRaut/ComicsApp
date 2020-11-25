using System.Threading.Tasks;
using ComicsApp.Server.ComicsService.ComicSources.DilbertComics.DilbertService;

namespace ComicsApp.Server.ComicsService.ComicSources.DilbertComics
{
    public class DilbertComics : IDilbertComics
    {
        public async Task<string> GetDilbertComicUri()
        {
            return await DilbertServiceApi.GetDilbertComicsUrl();
        }
    }
}