using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics.GarfieldService;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public string GetGarfieldComicUri()
        {
            GarfieldServiceApi garfieldServiceApi = new GarfieldServiceApi();
            return garfieldServiceApi.GetGarfieldComicsUrl();
        }
    }
}