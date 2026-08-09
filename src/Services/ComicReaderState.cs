using ComicsProvider;

namespace ComicsApp.Services;

// Per-user (scoped) endless feed of random comics pulled from every source.
public sealed class ComicReaderState(IComicsService comicsService)
{
    // RSS-backed sources have a finite pool of images, so we exhaust and drop them as they run dry.
    private static readonly (ComicEnum Source, string Label)[] RssSources =
    [
        (ComicEnum.Smbc, "SMBC"),
        (ComicEnum.DinosaurComics, "Dinosaur Comics"),
        (ComicEnum.PhdComics, "PHD Comics"),
        (ComicEnum.PoorlyDrawnLines, "Poorly Drawn Lines"),
        (ComicEnum.WarAndPeas, "War and Peas"),
        (ComicEnum.PerryBibleFellowship, "Perry Bible Fellowship")
    ];

    // Always-available sources; RSS feeds are added on top until each one is exhausted.
    private static readonly (ComicEnum Source, string Label)[] EndlessSources =
    [
        (ComicEnum.Xkcd, "XKCD"),
        (ComicEnum.Memes, "Memes")
    ];

    private readonly List<ComicItem> _items = [];
    private readonly HashSet<string> _shownUrls = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<ComicEnum> _exhaustedRss = [];
    private ComicEnum? _lastSource;

    public IReadOnlyList<ComicItem> Items => _items;

    public bool HasItems => _items.Count > 0;

    // Fetches a batch of comics from random sources, de-duplicating by URL and routing around any source
    // that fails (timeouts, 403s, bad feeds) or has no fresh images left.
    public async Task<int> LoadMoreAsync(int count = 3)
    {
        int added = 0;
        int attempts = 0;
        int maxAttempts = count * 6;

        while (added < count && attempts < maxAttempts)
        {
            attempts++;
            (ComicEnum source, string label) = PickSource();

            try
            {
                string? url = IsRss(source)
                    ? await PickFreshRssUrlAsync(source)
                    : await comicsService.GetComicAsync(source);

                if (string.IsNullOrWhiteSpace(url) || !_shownUrls.Add(url))
                {
                    continue;
                }

                _items.Add(new ComicItem(url, label));
                _lastSource = source;
                added++;
            }
            catch
            {
                // A flaky source shouldn't break the feed — just try another one.
            }
        }

        return added;
    }

    // Picks a random source, avoiding the previous one so consecutive comics come from different places.
    private (ComicEnum Source, string Label) PickSource()
    {
        var available = EndlessSources
            .Concat(RssSources.Where(s => !_exhaustedRss.Contains(s.Source)))
            .ToArray();

        var choices = available.Where(s => s.Source != _lastSource).ToArray();
        if (choices.Length == 0)
        {
            choices = available;
        }

        return choices[Random.Shared.Next(choices.Length)];
    }

    // Returns an unseen image URL from the feed, marking the source exhausted when nothing new remains.
    private async Task<string?> PickFreshRssUrlAsync(ComicEnum source)
    {
        var urls = await comicsService.GetRssImageUrlsAsync(source);
        var fresh = urls.Where(url => !_shownUrls.Contains(url)).ToArray();

        if (fresh.Length == 0)
        {
            _exhaustedRss.Add(source);
            return null;
        }

        return fresh[Random.Shared.Next(fresh.Length)];
    }

    private static bool IsRss(ComicEnum source) => source is not (ComicEnum.Xkcd or ComicEnum.Memes);

    // Clears the visible feed but keeps de-dup/exhaustion state so shuffled comics stay unique.
    public void Clear() => _items.Clear();
}

public sealed record ComicItem(string Url, string Source);
