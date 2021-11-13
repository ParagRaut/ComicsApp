namespace ComicsApp.Server.ComicsService;

public interface IComicUrlService
{
    Task<string> GetRandomComic();
    Task<string> GetDilbertComic();
    Task<string> GetGarfieldComic();
    Task<string> GetXkcdComic();
    Task<string> GetCalvinAndHobbesComic();
}
