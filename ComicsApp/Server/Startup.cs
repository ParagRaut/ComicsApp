using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using ComicsApp.Server.ComicsService;
using ComicsApp.Server.ComicsService.ComicSources.DilbertComics;
using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics;
using ComicsApp.Server.ComicsService.ComicSources.XKCD;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicServices.ComicSources.XKCD;
using Microsoft.AspNetCore.HttpOverrides;

namespace ComicsApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpClient<IXKCD, XKCD>();
            services.AddSingleton<IXKCD, XKCD>(p =>
            {
                HttpClient httpClient = p.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(nameof(IXKCD));

                return new XKCD(httpClient, true);
            });
            services.AddSingleton<IXkcdComic, XkcdComic>();
            services.AddSingleton<IGarfieldComics, GarfieldComics>();
            services.AddSingleton<IDilbertComics, DilbertComics>();
            services.AddSingleton<IComicUrlService, ComicUrlService>();
            services.AddSingleton<IComicService, ComicService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            loggerFactory.AddFile("Logs/ComicsApiLog-{Date}.log", LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }               

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
