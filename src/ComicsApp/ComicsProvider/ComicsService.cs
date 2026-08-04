using ComicsProvider.Imgflip;
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
    private const string WondermarkFeed = "https://wondermark.com/feed/";

    private readonly XKCDService _xkcdService;
    private readonly RssComicService _rssComicService;
    private readonly ImgflipService _imgflipService;

    public ComicsService(XKCDService xkcdService, RssComicService rssComicService, ImgflipService imgflipService)
    {
        _xkcdService = xkcdService;
        _rssComicService = rssComicService;
        _imgflipService = imgflipService;
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
        ComicEnum.Imgflip => _imgflipService.GetRandomImageAsync(),
        ComicEnum.PoorlyDrawnLines => _rssComicService.GetRandomImageAsync(PoorlyDrawnLinesFeed),
        ComicEnum.WarAndPeas => _rssComicService.GetRandomImageAsync(WarAndPeasFeed),
        ComicEnum.PerryBibleFellowship => _rssComicService.GetRandomImageAsync(PerryBibleFellowshipFeed),
        ComicEnum.Wondermark => _rssComicService.GetRandomImageAsync(WondermarkFeed),
        _ => throw new ArgumentOutOfRangeException(nameof(comic), comic, null)
    };
}
