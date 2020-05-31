using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.XKCD
{
    public interface IXkcdComic
    {
        Task<string> GetXkcdComicUri();
    }
}