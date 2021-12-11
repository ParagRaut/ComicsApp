using Refit;

namespace ComicsApp.Server.ComicsService.XKCD;

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

    public async Task<string> GetComicUri()
    {
        var comicId = await GetRandomComicNumber();

        var comic = await GetImageUri(comicId);

        return comic.img;
    }

    private async Task<int> GetRandomComicNumber()
    {
        var maxId = await GetLatestComicId();
        var randomNumber = new Random();
        return randomNumber.Next(maxId.num);
    }

    private async Task<XKCDComic> GetLatestComicId()
    {
        var response = await _xkcdService.GetLatestComic();

        return response;
    }

    private async Task<XKCDComic> GetImageUri(int comicId)
    {
        var comicImage = await _xkcdService.GetComicById(comicId);

        return comicImage;
    }
}

// ReSharper disable once InconsistentNaming
public record XKCDComic(int num, string img);