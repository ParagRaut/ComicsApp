using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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
            [NotNull] IDilbertComics dilbertComics,
            ILogger<ComicUrlService> logger)
        {
            XkcdComicsService = xkcdComic;
            GarfieldComicsService = garfieldComics;
            DilbertComicsService = dilbertComics;
            _logger = logger;
        }

        private IXkcdComic XkcdComicsService { get; }

        private IGarfieldComics GarfieldComicsService { get; }

        private IDilbertComics DilbertComicsService { get; }

        private Task<string> ComicImageUri { get; set; }

        private readonly ILogger _logger;

        public Task<string> GetRandomComic()
        {
            ComicEnum comicName = ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    ComicImageUri = GetGarfieldComic();
                    break;
                case ComicEnum.Xkcd:
                    ComicImageUri = GetXkcdComic();
                    break;
                case ComicEnum.Dilbert:
                    ComicImageUri = GetDilbertComic();
                    break;
                default:
                    _logger.LogInformation("Argument exception is thrown");
                    throw new ArgumentOutOfRangeException();
            }

            _logger.LogInformation($"Returning {comicName} comic strip");

            return ComicImageUri;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        public Task<string> GetDilbertComic()
        {
            _logger.LogInformation($"Returning Dilbert comic strip");
            return DilbertComicsService.GetDilbertComicUri();
        }

        public Task<string> GetGarfieldComic()
        {
            _logger.LogInformation($"Returning Garfield comic strip");

            return Task.Run(() =>
            {
                return GarfieldComicsService.GetGarfieldComicUri();
            });
        }

        public Task<string> GetXkcdComic()
        {
            _logger.LogInformation($"Returning XKCD comic strip");
            return XkcdComicsService.GetXkcdComicUri();
        }
    }
}
