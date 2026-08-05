namespace ComicsProvider;

public interface IComicsService
{
    Task<string> GetXkcdComics();

    Task<string> GetComicAsync(ComicEnum comic);

    // For RSS-backed sources, returns every distinct image URL currently in the feed (empty for non-RSS sources).
    Task<IReadOnlyList<string>> GetRssImageUrlsAsync(ComicEnum comic);
}
