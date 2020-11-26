using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.Garfield
{
    public class Garfield : IGarfield
    {
        public Task<string> GetGarfieldComicUri()
        {
            return Service.GetComicUri();        
        }
    }
}