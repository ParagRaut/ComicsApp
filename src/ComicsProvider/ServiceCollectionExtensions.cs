using ComicsProvider.Memes;
using ComicsProvider.Rss;
using ComicsProvider.XKCD;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Refit;

namespace ComicsProvider;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddComicsService(this IServiceCollection services)
    {
        services.AddRefitGeneratedClient<IXKCDService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://xkcd.com"));

        services.AddScoped<XKCDService>();

        // Some feeds (e.g. Poorly Drawn Lines) sit behind Cloudflare, which returns 403 for plain
        // HTTP/1.1 requests. Defaulting to HTTP/2 (plus browser-like headers) is what gets through.
        services.AddHttpClient<RssComicService>(client =>
        {
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0 Safari/537.36");
            client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
        });

        // meme-api.com is Cloudflare-fronted too, so use the same HTTP/2 + browser-header approach.
        services.AddHttpClient<MemeApiService>(client =>
        {
            client.BaseAddress = new Uri("https://meme-api.com/");
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0 Safari/537.36");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        });

        services.AddScoped<IComicsService, ComicsService>();

        return services;
    }
}
