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

    // Relative selection weights: memes dominate, XKCD is steady, and the RSS group shares the rest.
    private const double MemeWeight = 60;
    private const double XkcdWeight = 20;
    private const double RssGroupWeight = 20;

    private readonly List<ComicItem> _items = [];
    private readonly HashSet<string> _shownUrls = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<ComicEnum> _exhaustedRss = [];

    public IReadOnlyList<ComicItem> Items => _items;

    public bool HasItems => _items.Count > 0;

    // Fetches a batch of comics from weighted-random sources, de-duplicating by URL and routing around
    // any source that fails (timeouts, 403s, bad feeds) or has no fresh images left.
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
                added++;
            }
            catch
            {
                // A flaky source shouldn't break the feed — just try another one.
            }
        }

        return added;
    }

    // Picks a source using the configured weights; the RSS group is skipped entirely once every feed is exhausted.
    private (ComicEnum Source, string Label) PickSource()
    {
        var activeRss = RssSources.Where(s => !_exhaustedRss.Contains(s.Source)).ToArray();
        double rssWeight = activeRss.Length > 0 ? RssGroupWeight : 0;
        double roll = Random.Shared.NextDouble() * (MemeWeight + XkcdWeight + rssWeight);

        if (roll < MemeWeight)
        {
            return (ComicEnum.Memes, "Memes");
        }

        if (roll < MemeWeight + XkcdWeight)
        {
            return (ComicEnum.Xkcd, "XKCD");
        }

        return activeRss[Random.Shared.Next(activeRss.Length)];
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
