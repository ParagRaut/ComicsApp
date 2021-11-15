namespace ComicsApp.Server.ComicsService.ComicSources.XKCD
{
    public interface IXKCDService
    {
        Task<string> GetComicUri();
    }
}
