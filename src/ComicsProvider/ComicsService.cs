using ComicsProvider.XKCD;

namespace ComicsProvider;

public class ComicsService : IComicsService
{
    private readonly XKCDService _xkcdService;

    public ComicsService(XKCDService xkcdService)
    {
        _xkcdService = xkcdService;
    }

    public async Task<string> GetXkcdComics()
    {
        return await _xkcdService.GetComicUri();
    }
}