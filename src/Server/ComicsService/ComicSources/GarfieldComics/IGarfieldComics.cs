using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics
{
    public interface IGarfieldComics
    {
        Task<string> GetGarfieldComicUri();
    }
}