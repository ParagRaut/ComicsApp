using ComicsProvider.CalvinAndHobbes;
using ComicsProvider.Garfield;
using ComicsProvider.XKCD;

namespace ComicsProvider;

public class ComicsService : IComicsService
{
    private readonly GarfieldService _garfieldService;
    private readonly CalvinAndHobbesService _calvinAndHobbesService;
    private readonly XKCDService _xkcdService;

    public ComicsService(
        GarfieldService garfieldService,
        CalvinAndHobbesService calvinAndHobbesService,
        XKCDService xkcdService)
    {
        _garfieldService = garfieldService;
        _calvinAndHobbesService = calvinAndHobbesService;
        _xkcdService = xkcdService;
    }

    public async Task<string> GetXkcdComics()
    {
        return await _xkcdService.GetComicUri();
    }

    public async Task<string> GetGarfieldComics()
    {
        return await _garfieldService.GetComicUri();
    }

    public async Task<string> GetCalvinAndHobbesComics()
    {
        return await _calvinAndHobbesService.GetComicUri();
    }
}