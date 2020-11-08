using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes
{
    public interface ICalvinAndHobbesComics
    {
        Task<string> CalvinAndHobbesComicUri();
    }
}
