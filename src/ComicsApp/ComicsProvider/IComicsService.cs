namespace ComicsProvider;

public interface IComicsService
{
    Task<string> GetXkcdComics();

    Task<string> GetComicAsync(ComicEnum comic);
}
