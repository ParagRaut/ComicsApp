using ComicsApp.Server.ComicsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComicsApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public string Get()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicUrlService.GetRandomComic();
        }
    }
}