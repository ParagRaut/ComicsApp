using ComicsProvider;
using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.Controllers;

[ApiController]
public class ComicsController : ControllerBase
{
    public ComicsController(IComicsService service,
        ILogger<ComicsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    private readonly IComicsService _service;
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
            ComicEnum.Xkcd => GetXkcd(),
            ComicEnum.CalvinAndHobbes => GetCalvinAndHobbes(),
            _ => throw new ArgumentOutOfRangeException()
        };        
    }

    [HttpGet]
    [Route("[controller]/garfield")]
    public Task<string> GetGarfield()
    {
        _logger.LogInformation("Fetching garfield comic...");

        return _service.GetGarfieldComics();
    }

    [HttpGet]
    [Route("[controller]/xkcd")]
    public Task<string> GetXkcd()
    {
        _logger.LogInformation("Fetching xkcd comic...");

        return _service.GetXkcdComics();
    }

    [HttpGet]
    [Route("[controller]/calvinandhobbes")]
    public Task<string> GetCalvinAndHobbes()
    {
        _logger.LogInformation($"Fetching Calvin and Hobbes comic strip");

        return _service.GetCalvinAndHobbesComics();
    }

    private static ComicEnum ChooseRandomComicSource()
    {
        var random = new Random();
        return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
    }
}
