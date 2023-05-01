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
        services.AddHttpClient<CalvinAndHobbesService>(
            client => client.BaseAddress = new Uri("https://www.gocomics.com/random/"));

        services.AddHttpClient<GarfieldService>(
            client => client.BaseAddress = new Uri("https://www.gocomics.com/garfield/"));

        services.AddRefitClient<IXKCDService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://xkcd.com"));

        services.AddScoped<XKCDService>();

        services.AddScoped<IComicsService, ComicsService>();

        return services;
    }
}
