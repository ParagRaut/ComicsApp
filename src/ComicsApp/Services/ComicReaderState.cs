using ComicsProvider;

namespace ComicsApp.Services;

// Per-user (scoped) history of viewed comics with navigation across multiple sources.
public sealed class ComicReaderState(IComicsService comicsService)
{
    private const string PlaceholderImage = "images/xkcd.jpg";

    private readonly List<string> _comics = [PlaceholderImage];

    public string CurrentComic => _comics[CurrentIndex];

    public int CurrentIndex { get; private set; }

    public bool CanGoBack => CurrentIndex > 0;

    public ComicEnum Source { get; set; } = ComicEnum.Xkcd;

    public void GoToPrevious()
    {
        if (CanGoBack)
        {
            CurrentIndex--;
        }
    }

    // Steps forward through already-loaded history, otherwise fetches a new comic from the selected source.
    public async Task GoToNextAsync()
    {
        if (CurrentIndex < _comics.Count - 1)
        {
            CurrentIndex++;
            return;
        }

        string comic = await comicsService.GetComicAsync(Source);

        if (!string.IsNullOrWhiteSpace(comic))
        {
            _comics.Add(comic);
            CurrentIndex = _comics.Count - 1;
        }
    }
}
