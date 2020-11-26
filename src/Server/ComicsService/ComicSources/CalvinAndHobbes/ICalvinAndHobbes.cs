using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes
{
    public interface ICalvinAndHobbes
    {
        Task<string> CalvinAndHobbesComicUri();
    }
}
