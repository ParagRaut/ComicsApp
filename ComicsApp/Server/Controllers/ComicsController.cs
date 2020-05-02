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
        public string GetRandom()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public string GetDilbert()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public string GetGarfield()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public string GetXkcd()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetXkcdComic();
        }
    }
}