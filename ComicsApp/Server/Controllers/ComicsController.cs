using System.Threading.Tasks;
using ComicsApp.Server.ComicsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComicsApp.Server.Controllers
{
    [ApiController]
    public class ComicsController : ControllerBase
    {
        public ComicsController(IComicUrlService comicService, ILogger<ComicsController> logger)
        {
            this.ComicUrlService = comicService;
            this._logger = logger;
        }

        private IComicUrlService ComicUrlService { get; }
        private readonly ILogger _logger;

        [HttpGet]
        [Route("[controller]/random")]
        public Task<string> GetRandom()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public Task<string> GetDilbert()
        {
            this._logger.LogInformation("Fetching dilbert comic...");
            return this.ComicUrlService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public Task<string> GetGarfield()
        {
            this._logger.LogInformation("Fetching garfield comic...");
            return this.ComicUrlService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public Task<string> GetXkcd()
        {
            this._logger.LogInformation("Fetching xkcd comic...");
            return this.ComicUrlService.GetXkcdComic();
        }

        [HttpGet]
        [Route("[controller]/calvinandhobbes")]
        public Task<string> GetCalvinAndHobbesComic()
        {
            this._logger.LogInformation($"Fetching Calvin and Hobbes comic strip");
            return this.ComicUrlService.GetCalvinAndHobbesComic();
        }
    }
}