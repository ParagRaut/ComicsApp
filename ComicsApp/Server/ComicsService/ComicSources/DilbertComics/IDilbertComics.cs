using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        Task<string> GetDilbertComicUri();
    }
}