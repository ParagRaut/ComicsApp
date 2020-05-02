using System;
using System.Diagnostics.CodeAnalysis;
using ComicsApp.Server.ComicsService.ComicSources;
using ComicsApp.Server.ComicsService.ComicSources.DilbertComics;
using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics;
using ComicsApp.Server.ComicsService.ComicSources.XKCD;
using Microsoft.Extensions.Logging;

namespace ComicsApp.Server.ComicsService
{
    public class ComicUrlService : IComicUrlService
    {
        public ComicUrlService([NotNull] IXkcdComic xkcdComic,
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

        private string ComicImageUri { get; set; }

        private readonly ILogger _logger;

        public string GetRandomComic()
        {
            ComicEnum comicName = this.ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    this.ComicImageUri = this.GetGarfieldComicUri();
                    break;
                case ComicEnum.Xkcd:
                    this.ComicImageUri = this.GetXkcdComicUri();
                    break;
                case ComicEnum.Dilbert:
                    this.ComicImageUri = this.GetDilbertComicUri();
                    break;
                default:
                    this._logger.LogInformation("Argument exception is thrown");
                    throw new ArgumentOutOfRangeException();
            }

            this._logger.LogInformation($"Returning {comicName} comic strip");

            return this.ComicImageUri;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        private string GetXkcdComicUri()
        {
            this.ComicImageUri = this.XkcdComicsService.GetXkcdComicUri();
            return this.ComicImageUri;
        }

        private string GetGarfieldComicUri()
        {
            this.ComicImageUri = this.GarfieldComicsService.GetGarfieldComicUri();
            return this.ComicImageUri;
        }

        private string GetDilbertComicUri()
        {
            this.ComicImageUri = this.DilbertComicsService.GetDilbertComicUri();
            return this.ComicImageUri;
        }

        public string GetDilbertComic()
        {
            return this.GetDilbertComicUri();
        }

        public string GetGarfieldComic()
        {
            return this.GetGarfieldComicUri();
        }

        public string GetXkcdComic()
        {
            return this.GetXkcdComicUri();
        }
    }

}
