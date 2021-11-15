namespace ComicsApp.Server.ComicsService.XKCD;

public interface IXKCDService
{
    Task<string> GetComicUri();
}
