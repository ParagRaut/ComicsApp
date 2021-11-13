using System.Diagnostics.CodeAnalysis;
using ComicsApp.Server.ComicsService.ComicSources;
using ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.ComicSources.Dilbert;
using ComicsApp.Server.ComicsService.ComicSources.Garfield;
using ComicsApp.Server.ComicsService.ComicSources.Xkcd;

namespace ComicsApp.Server.ComicsService;

public class ComicUrlService : IComicUrlService
{
    public ComicUrlService(
        [NotNull] IXkcdComic xkcd,
        [NotNull] IGarfield garfield,
        [NotNull] IDilbert dilbert,
        [NotNull] ICalvinAndHobbes calvinAndHobbes,
        ILogger<ComicUrlService> logger)
    {
        _xkcd = xkcd;
        _garfield = garfield;
        _dilbert = dilbert;
        _calvinAndHobbes = calvinAndHobbes;
        _logger = logger;
    }

    private readonly IXkcdComic _xkcd;
    private readonly IGarfield _garfield;
    private readonly IDilbert _dilbert;
    private readonly ICalvinAndHobbes _calvinAndHobbes;
    private readonly ILogger _logger;

    public Task<string> GetRandomComic()
    {
        ComicEnum comicName = ChooseRandomComicSource();

        return comicName switch
        {
            ComicEnum.Garfield => GetGarfieldComic(),
            ComicEnum.Xkcd => GetXkcdComic(),
            ComicEnum.Dilbert => GetDilbertComic(),
            ComicEnum.CalvinAndHobbes => GetCalvinAndHobbesComic(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static ComicEnum ChooseRandomComicSource()
    {
        var random = new Random();
        return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
    }

    public Task<string> GetDilbertComic()
    {
        _logger.LogInformation($"Returning Dilbert comic strip");

        return _dilbert.GetDilbertComicUri();
    }

    public Task<string> GetGarfieldComic()
    {
        _logger.LogInformation($"Returning Garfield comic strip");

        return _garfield.GetGarfieldComicUri();
    }

    public Task<string> GetXkcdComic()
    {
        _logger.LogInformation($"Returning XKCD comic strip");

        return _xkcd.GetXkcdComicUri();
    }

    public Task<string> GetCalvinAndHobbesComic()
    {
        _logger.LogInformation($"Returning Calvin and Hobbes comic strip");

        return _calvinAndHobbes.CalvinAndHobbesComicUri();
    }
}
