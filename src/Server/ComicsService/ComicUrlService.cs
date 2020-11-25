using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ComicsApp.Server.ComicsService.ComicSources;
using ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes;
using ComicsApp.Server.ComicsService.ComicSources.DilbertComics;
using ComicsApp.Server.ComicsService.ComicSources.GarfieldComics;
using ComicsApp.Server.ComicsService.ComicSources.XKCD;
using Microsoft.Extensions.Logging;

namespace ComicsApp.Server.ComicsService
{
    public class ComicUrlService : IComicUrlService
    {
        public ComicUrlService(
            [NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfieldComics garfieldComics,
            [NotNull] IDilbertComics dilbertComics,
            [NotNull] ICalvinAndHobbesComics calvinAndHobbesComics,
            ILogger<ComicUrlService> logger)
        {
            this._xkcdComic = xkcdComic;
            this._garfieldComics = garfieldComics;
            this._dilbertComics = dilbertComics;
            this._calvinAndHobbesComics = calvinAndHobbesComics;
            _logger = logger;
        }

        private readonly IXkcdComic _xkcdComic;
        private readonly IGarfieldComics _garfieldComics;
        private readonly IDilbertComics _dilbertComics;
        private readonly ICalvinAndHobbesComics _calvinAndHobbesComics;
        private readonly ILogger _logger;

        public Task<string> GetRandomComic()
        {
            ComicEnum comicName = this.ChooseRandomComicSource();

            return comicName switch
            {
                ComicEnum.Garfield => this.GetGarfieldComic(),
                ComicEnum.Xkcd => this.GetXkcdComic(),
                ComicEnum.Dilbert => this.GetDilbertComic(),
                ComicEnum.CalvinAndHobbes => this.GetCalvinAndHobbesComic(),
                _ => throw new ArgumentOutOfRangeException()
            };            
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        public Task<string> GetDilbertComic()
        {
            this._logger.LogInformation($"Returning Dilbert comic strip");

            return this._dilbertComics.GetDilbertComicUri();
        }

        public Task<string> GetGarfieldComic()
        {
            this._logger.LogInformation($"Returning Garfield comic strip");

            return this._garfieldComics.GetGarfieldComicUri();
        }

        public Task<string> GetXkcdComic()
        {
            this._logger.LogInformation($"Returning XKCD comic strip");

            return this._xkcdComic.GetXkcdComicUri();
        }

        public Task<string> GetCalvinAndHobbesComic()
        {
            this._logger.LogInformation($"Returning Calvin and Hobbes comic strip");

            return this._calvinAndHobbesComics.CalvinAndHobbesComicUri();
        }
    }
}
