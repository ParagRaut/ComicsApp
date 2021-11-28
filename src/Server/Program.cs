﻿using ComicsApp.Server.ComicsService.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.Dilbert;
using ComicsApp.Server.ComicsService.Garfield;
using ComicsApp.Server.ComicsService.XKCD;
using ComicsApp.Server.ComicsService.XKCD.Generated;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<CalvinAndHobbesService>(client => client.BaseAddress = new Uri("https://www.gocomics.com/random/"));

builder.Services.AddHttpClient<DilbertService>(client => client.BaseAddress = new Uri("https://dilbert.com/strip/"));

builder.Services.AddHttpClient<GarfieldService>(client => client.BaseAddress = new Uri("https://www.gocomics.com/garfield/"));

builder.Services.AddScoped<IXKCD, XKCD>(p => new XKCD(new HttpClient(), true));

builder.Services.AddScoped<XKCDService>();

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