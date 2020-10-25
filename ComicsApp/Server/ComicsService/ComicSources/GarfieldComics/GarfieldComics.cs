using System.Threading.Tasks;
using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics.GarfieldService;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public Task<string> GetGarfieldComicUri()
        {
            var garfieldServiceApi = new GarfieldServiceApi();
            return garfieldServiceApi.GetGarfieldComicsUrl();        
        }
    }
}