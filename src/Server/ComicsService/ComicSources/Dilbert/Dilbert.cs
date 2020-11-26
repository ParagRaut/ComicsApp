using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.Dilbert
{
    public class Dilbert : IDilbert
    {
        public async Task<string> GetDilbertComicUri()
        {
            return await Service.GetComicUri();
        }
    }
}