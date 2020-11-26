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
            _comicService = comicService;
            _logger = logger;
        }

        private readonly IComicUrlService _comicService;
        private readonly ILogger _logger;

        [HttpGet]
        [Route("[controller]/random")]
        public Task<string> GetRandom()
        {
            _logger.LogInformation("Fetching random comic...");

            return _comicService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public Task<string> GetDilbert()
        {
            _logger.LogInformation("Fetching dilbert comic...");

            return _comicService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public Task<string> GetGarfield()
        {
            _logger.LogInformation("Fetching garfield comic...");

            return _comicService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public Task<string> GetXkcd()
        {
            _logger.LogInformation("Fetching xkcd comic...");

            return _comicService.GetXkcdComic();
        }

        [HttpGet]
        [Route("[controller]/calvinandhobbes")]
        public Task<string> GetCalvinAndHobbesComic()
        {
            _logger.LogInformation($"Fetching Calvin and Hobbes comic strip");

            return _comicService.GetCalvinAndHobbesComic();
        }
    }
}