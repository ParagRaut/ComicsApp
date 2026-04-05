using ComicsProvider.CalvinAndHobbes;
using ComicsProvider.Garfield;
using ComicsProvider.XKCD;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ComicsProvider;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddComicsService(this IServiceCollection services)
    {
        services.AddHttpClient<CalvinAndHobbesService>(client =>
        {
            client.BaseAddress = new Uri("https://www.gocomics.com/calvinandhobbes");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible)");
        });

        services.AddHttpClient<GarfieldService>(client =>
        {
            client.BaseAddress = new Uri("https://www.gocomics.com/garfield");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible)");
        });

        services.AddRefitClient<IXKCDService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://xkcd.com"));

        services.AddScoped<XKCDService>();

        services.AddScoped<IComicsService, ComicsService>();

        return services;
    }
}
