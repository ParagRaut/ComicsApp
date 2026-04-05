namespace ComicsProvider;

public interface IComicsService
{
    Task<string> GetXkcdComics();
}
