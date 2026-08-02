using ComicsProvider;

namespace ComicsApp.Services;

// Per-user (scoped) history of viewed XKCD comics with navigation.
public sealed class XkcdComicState(IComicsService comicsService)
{
    private const string PlaceholderImage = "images/xkcd.jpg";

    private readonly List<string> _comics = [PlaceholderImage];

    public string CurrentComic => _comics[CurrentIndex];

    public int CurrentIndex { get; private set; }

    public bool CanGoBack => CurrentIndex > 0;

    public void GoToPrevious()
    {
        if (CanGoBack)
        {
            CurrentIndex--;
        }
    }

    // Steps forward through already-loaded history, otherwise fetches a new comic.
    public async Task GoToNextAsync()
    {
        if (CurrentIndex < _comics.Count - 1)
        {
            CurrentIndex++;
            return;
        }

        string comic = await comicsService.GetXkcdComics();

        if (!string.IsNullOrWhiteSpace(comic))
        {
            _comics.Add(comic);
            CurrentIndex = _comics.Count - 1;
        }
    }
}
