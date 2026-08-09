using System.Net.Http.Json;

namespace ComicsProvider.Memes;

// Fetches a random meme image URL from meme-api.com (a server-side Reddit meme proxy).
public sealed class MemeApiService
{
    private readonly HttpClient _httpClient;

    public MemeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRandomImageAsync(CancellationToken cancellationToken = default)
    {
        var meme = await _httpClient.GetFromJsonAsync<MemeResponse>("gimme", cancellationToken);
        return meme?.Url ?? string.Empty;
    }
}

public sealed record MemeResponse(string Url);
