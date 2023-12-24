using Refit;

namespace ComicsProvider.XKCD;

// ReSharper disable once InconsistentNaming
public interface IXKCDService
{
    [Get("/info.0.json")]
    Task<XKCDComic> GetLatestComic();

    [Get("/{comicId}/info.0.json")]
    Task<XKCDComic> GetComicById(int comicId);
}

// ReSharper disable once InconsistentNaming
public class XKCDService
{
    private readonly IXKCDService _xkcdService;

    public XKCDService(IXKCDService xkcdService)
    {
        _xkcdService = xkcdService;
    }    

    protected internal async Task<string> GetComicUri()
    {
        var comicId = await this.GetRandomComicNumber();

        var comic = await this.GetImageUri(comicId);

        return comic.img;
    }

    private async Task<int> GetRandomComicNumber()
    {
        var maxId = await this.GetLatestComicId();
        var randomNumber = new Random();
        return randomNumber.Next(maxId.num);
    }

    private async Task<XKCDComic> GetLatestComicId()
    {
        var response = await this._xkcdService.GetLatestComic();

        return response;
    }

    private async Task<XKCDComic> GetImageUri(int comicId)
    {
        var comicImage = await this._xkcdService.GetComicById(comicId);

        return comicImage;
    }
}

// ReSharper disable once InconsistentNaming
public record XKCDComic(int num, string img);