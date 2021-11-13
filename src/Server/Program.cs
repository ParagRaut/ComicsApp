using ComicsApp.Server.ComicsService;
using ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.ComicSources.Dilbert;
using ComicsApp.Server.ComicsService.ComicSources.Garfield;
using ComicsApp.Server.ComicsService.ComicSources.Xkcd;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IXKCD, XKCD>();
builder.Services.AddSingleton<IXKCD, XKCD>(p =>
{
    HttpClient httpClient = p.GetRequiredService<IHttpClientFactory>()
        .CreateClient(nameof(IXKCD));

    return new XKCD(httpClient, true);
});
builder.Services.AddSingleton<IXkcdComic, XkcdComic>();
builder.Services.AddSingleton<IGarfield, Garfield>();
builder.Services.AddSingleton<IDilbert, Dilbert>();
builder.Services.AddSingleton<ICalvinAndHobbes, CalvinAndHobbes>();
builder.Services.AddSingleton<IComicUrlService, ComicUrlService>();            

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();