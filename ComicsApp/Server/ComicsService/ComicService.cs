using System;
using System.Diagnostics.CodeAnalysis;
using ComicsApp.Server.ComicsService.ComicSources;
using ComicsApp.Server.ComicsService.ComicSources.DilbertComics;
using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics;
using ComicsApp.Server.ComicsService.ComicSources.XKCD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComicsApp.Server.ComicsService
{
    public class ComicService : IComicService
    {
        public ComicService([NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfieldComics garfieldComics,
            [NotNull] IDilbertComics gDilbertComics,
            ILogger<ComicService> logger)
        {
            this.XkcdComicsService = xkcdComic;
            this.GarfieldComicsService = garfieldComics;
            this.DilbertComicsService = gDilbertComics;
            this._logger = logger;
        }

        private IXkcdComic XkcdComicsService { get; }

        private IGarfieldComics GarfieldComicsService { get; }

        private IDilbertComics DilbertComicsService { get; }

        private FileResult ComicImage { get; set; }

        private readonly ILogger _logger;


        public FileResult GetRandomComic()
        {
            ComicEnum comicName = this.ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    this.ComicImage = this.GetGarfieldComic();
                    break;
                case ComicEnum.Xkcd:
                    this.ComicImage = this.GetXkcdComic();
                    break;
                case ComicEnum.Dilbert:
                    this.ComicImage = this.GetDilbertComic();
                    break;
                default:
                    this._logger.LogInformation("Argument exception is thrown");
                    throw new ArgumentOutOfRangeException();
            }

            this._logger.LogInformation($"Returning {comicName} comic strip");

            return this.ComicImage;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum) random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        private FileResult GetXkcdComic()
        {
            this.ComicImage = this.XkcdComicsService.GetXkcdComic();
            return this.ComicImage;
        }

        private FileResult GetGarfieldComic()
        {
            this.ComicImage = this.GarfieldComicsService.GetGarfieldComic();
            return this.ComicImage;
        }

        private FileResult GetDilbertComic()
        {
            this.ComicImage = this.DilbertComicsService.GetDilbertComic();
            return this.ComicImage;
        }
    }
}