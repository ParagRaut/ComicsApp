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
    public ComicsController(XKCDService xKCDService,
        CalvinAndHobbesService calvinAndHobbesService,
        DilbertService dilbertService,
        GarfieldService garfieldService,
        ILogger<ComicsController> logger)
    {
        _xKCDService = xKCDService;
        _calvinAndHobbesService = calvinAndHobbesService;
        _dilbertService = dilbertService;
        _garfieldService = garfieldService;
        _logger = logger;
    }

    private readonly XKCDService _xKCDService;
    private readonly CalvinAndHobbesService _calvinAndHobbesService;
    private readonly DilbertService _dilbertService;
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

        return _dilbertService.GetComicUri();
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

        return _xKCDService.GetComicUri();
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
