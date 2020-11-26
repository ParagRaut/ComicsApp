using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.Dilbert
{
    public interface IDilbert
    {
        Task<string> GetDilbertComicUri();
    }
}