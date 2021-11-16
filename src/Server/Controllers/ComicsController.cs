using ComicsApp.Server.ComicsService;
using ComicsApp.Server.ComicsService.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.Dilbert;
using ComicsApp.Server.ComicsService.Garfield;
using ComicsApp.Server.ComicsService.XKCD;
using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.Controllers;

[ApiController]
public class ComicsController : ControllerBase
{
    public ComicsController(XKCDService service, ILogger<ComicsController> logger)
    {
        _comicService = service;
        _logger = logger;
    }

    private readonly XKCDService _comicService;
    private readonly ILogger _logger;

    [HttpGet]
    [Route("[controller]/random")]
    public Task<string> GetRandom()
    {
        _logger.LogInformation("Fetching random comic...");

        ComicEnum comicName = ChooseRandomComicSource();

        return comicName switch
        {
            ComicEnum.Garfield => GetGarfield(),
            ComicEnum.Xkcd => GetXKCD(),
            ComicEnum.Dilbert => GetDilbert(),
            ComicEnum.CalvinAndHobbes => GetCalvinAndHobbesComic(),
            _ => throw new ArgumentOutOfRangeException()
        };        
    }

    [HttpGet]
    [Route("[controller]/dilbert")]
    public Task<string> GetDilbert()
    {
        _logger.LogInformation("Fetching dilbert comic...");

        return DilbertService.GetComicUri();
    }

    [HttpGet]
    [Route("[controller]/garfield")]
    public Task<string> GetGarfield()
    {
        _logger.LogInformation("Fetching garfield comic...");

        return GarfieldService.GetComicUri();
    }

    [HttpGet]
    [Route("[controller]/xkcd")]
    public Task<string> GetXKCD()
    {
        _logger.LogInformation("Fetching xkcd comic...");

        return _comicService.GetComicUri();
    }

    [HttpGet]
    [Route("[controller]/calvinandhobbes")]
    public Task<string> GetCalvinAndHobbesComic()
    {
        _logger.LogInformation($"Fetching Calvin and Hobbes comic strip");

        return CalvinAndHobbesService.GetComicUri();
    }

    private static ComicEnum ChooseRandomComicSource()
    {
        var random = new Random();
        return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
    }
}
