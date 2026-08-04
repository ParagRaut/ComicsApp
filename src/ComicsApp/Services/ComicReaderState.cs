using ComicsProvider;

namespace ComicsApp.Services;

// Per-user (scoped) endless feed of random comics pulled from every source.
public sealed class ComicReaderState(IComicsService comicsService)
{
    private static readonly (ComicEnum Source, string Label)[] AllSources =
    [
        (ComicEnum.Xkcd, "XKCD"),
        (ComicEnum.Smbc, "SMBC"),
        (ComicEnum.DinosaurComics, "Dinosaur Comics"),
        (ComicEnum.PhdComics, "PHD Comics"),
        (ComicEnum.Imgflip, "imgflip"),
        (ComicEnum.PoorlyDrawnLines, "Poorly Drawn Lines"),
        (ComicEnum.WarAndPeas, "War and Peas"),
        (ComicEnum.PerryBibleFellowship, "Perry Bible Fellowship"),
        (ComicEnum.Wondermark, "Wondermark")
    ];

    private readonly List<ComicItem> _items = [];

    public IReadOnlyList<ComicItem> Items => _items;

    public bool HasItems => _items.Count > 0;

    // Fetches a batch of comics from randomly chosen sources, routing around any that fail (timeouts, 403s, bad feeds).
    public async Task<int> LoadMoreAsync(int count = 3)
    {
        int added = 0;
        int attempts = 0;
        int maxAttempts = count * 4;

        while (added < count && attempts < maxAttempts)
        {
            attempts++;
            (ComicEnum source, string label) = AllSources[Random.Shared.Next(AllSources.Length)];

            try
            {
                string url = await comicsService.GetComicAsync(source);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    _items.Add(new ComicItem(url, label));
                    added++;
                }
            }
            catch
            {
                // A flaky source shouldn't break the feed — just try another one.
            }
        }

        return added;
    }

    public void Clear() => _items.Clear();
}

public sealed record ComicItem(string Url, string Source);
