using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.Xkcd;

public interface IXkcdComic
{
    Task<string> GetXkcdComicUri();
}
