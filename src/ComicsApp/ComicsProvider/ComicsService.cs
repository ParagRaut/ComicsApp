using ComicsProvider.Memes;
using ComicsProvider.Rss;
using ComicsProvider.XKCD;

namespace ComicsProvider;

public class ComicsService : IComicsService
{
    private const string SmbcFeed = "https://www.smbc-comics.com/comic/rss";
    private const string DinosaurComicsFeed = "https://www.qwantz.com/rssfeed.php";
    private const string PhdComicsFeed = "https://phdcomics.com/gradfeed.php";
    private const string PoorlyDrawnLinesFeed = "https://poorlydrawnlines.com/feed/";
    private const string WarAndPeasFeed = "https://warandpeas.com/feed/";
    private const string PerryBibleFellowshipFeed = "https://pbfcomics.com/feed/";

    private readonly XKCDService _xkcdService;
    private readonly RssComicService _rssComicService;
    private readonly MemeApiService _memeApiService;

    public ComicsService(XKCDService xkcdService, RssComicService rssComicService, MemeApiService memeApiService)
    {
        _xkcdService = xkcdService;
        _rssComicService = rssComicService;
        _memeApiService = memeApiService;
    }

    public async Task<string> GetXkcdComics()
    {
        return await _xkcdService.GetComicUri();
    }

    public Task<string> GetComicAsync(ComicEnum comic) => comic switch
    {
        ComicEnum.Xkcd => _xkcdService.GetComicUri(),
        ComicEnum.Smbc => _rssComicService.GetRandomImageAsync(SmbcFeed),
        ComicEnum.DinosaurComics => _rssComicService.GetRandomImageAsync(DinosaurComicsFeed),
        ComicEnum.PhdComics => _rssComicService.GetRandomImageAsync(PhdComicsFeed),
        ComicEnum.PoorlyDrawnLines => _rssComicService.GetRandomImageAsync(PoorlyDrawnLinesFeed),
        ComicEnum.WarAndPeas => _rssComicService.GetRandomImageAsync(WarAndPeasFeed),
        ComicEnum.PerryBibleFellowship => _rssComicService.GetRandomImageAsync(PerryBibleFellowshipFeed),
        ComicEnum.Memes => _memeApiService.GetRandomImageAsync(),
        _ => throw new ArgumentOutOfRangeException(nameof(comic), comic, null)
    };

    public Task<IReadOnlyList<string>> GetRssImageUrlsAsync(ComicEnum comic) => comic switch
    {
        ComicEnum.Smbc => _rssComicService.GetAllImageUrlsAsync(SmbcFeed),
        ComicEnum.DinosaurComics => _rssComicService.GetAllImageUrlsAsync(DinosaurComicsFeed),
        ComicEnum.PhdComics => _rssComicService.GetAllImageUrlsAsync(PhdComicsFeed),
        ComicEnum.PoorlyDrawnLines => _rssComicService.GetAllImageUrlsAsync(PoorlyDrawnLinesFeed),
        ComicEnum.WarAndPeas => _rssComicService.GetAllImageUrlsAsync(WarAndPeasFeed),
        ComicEnum.PerryBibleFellowship => _rssComicService.GetAllImageUrlsAsync(PerryBibleFellowshipFeed),
        _ => Task.FromResult<IReadOnlyList<string>>([])
    };
}
