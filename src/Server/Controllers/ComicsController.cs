using ComicsApp.Server.ComicsService;
using ComicsApp.Server.ComicsService.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.Garfield;
using ComicsApp.Server.ComicsService.XKCD;
using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.Controllers;

[ApiController]
public class ComicsController : ControllerBase
{
    public ComicsController(XKCDService xKCDService,
        CalvinAndHobbesService calvinAndHobbesService,
        GarfieldService garfieldService,
        ILogger<ComicsController> logger)
    {
        _xKcdService = xKCDService;
        _calvinAndHobbesService = calvinAndHobbesService;
        _garfieldService = garfieldService;
        _logger = logger;
    }

    private readonly XKCDService _xKcdService;
    private readonly CalvinAndHobbesService _calvinAndHobbesService;
    private readonly GarfieldService _garfieldService;
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
            ComicEnum.CalvinAndHobbes => GetCalvinAndHobbesComic(),
            _ => throw new ArgumentOutOfRangeException()
        };        
    }

    [HttpGet]
    [Route("[controller]/garfield")]
    public Task<string> GetGarfield()
    {
        _logger.LogInformation("Fetching garfield comic...");

        return _garfieldService.GetComicUri();
    }

    [HttpGet]
    [Route("[controller]/xkcd")]
    public Task<string> GetXKCD()
    {
        _logger.LogInformation("Fetching xkcd comic...");

        return this._xKcdService.GetComicUri();
    }

    [HttpGet]
    [Route("[controller]/calvinandhobbes")]
    public Task<string> GetCalvinAndHobbesComic()
    {
        _logger.LogInformation($"Fetching Calvin and Hobbes comic strip");

        return _calvinAndHobbesService.GetComicUri();
    }

    private static ComicEnum ChooseRandomComicSource()
    {
        var random = new Random();
        return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
    }
}
