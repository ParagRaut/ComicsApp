using System.Diagnostics;
using ComicsApp.Server.ComicsService.XKCD.Generated;
using ComicsApp.Server.ComicsService.XKCD.Generated.Models;

namespace ComicsApp.Server.ComicsService.XKCD;

public class XKCDService : IXKCDService
{
    public XKCDService(IXKCD xKcdComics)
    {
        Service = xKcdComics;
    }

    private IXKCD Service { get; }

    public async Task<string> GetComicUri()
    {
        var comicId = await GetRandomComicNumber();

        string comicImageUri = await GetImageUri(comicId);

        return comicImageUri;
    }

    private async Task<int> GetLatestComicId()
    {
        Comic response = await Service.GetLatestComicAsync();

        Debug.Assert(response.Num != null, "response.Num != null");

        return (int)response.Num.Value;
    }

    private async Task<int> GetRandomComicNumber()
    {
        var maxId = await GetLatestComicId();
        var randomNumber = new Random();
        return randomNumber.Next(maxId);
    }

    private async Task<string> GetImageUri(int comicId)
    {
        Comic comicImage = await Service.GetComicByIdAsync(comicId).ConfigureAwait(false);

        return comicImage.Img;
    }
}
