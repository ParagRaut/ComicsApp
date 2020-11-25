using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ComicsApp.Server.ComicsService.ComicSources.XKCD.Models;

namespace ComicsApp.Server.ComicsService.ComicSources.XKCD
{
    public class XkcdComic : IXkcdComic
    {
        public XkcdComic(IXKCD xKcdComics)
        {
            this.XkcdService = xKcdComics;
        }

        private IXKCD XkcdService { get; }

        public async Task<string> GetXkcdComicUri()
        {
            var comicId = await this.GetRandomComicNumber();

            string comicImageUri = await this.GetImageUri(comicId);
            
            return comicImageUri;
        }

        private async Task<int> GetLatestComicId()
        {
            Comic response = await this.XkcdService.GetLatestComicAsync();

            Debug.Assert(response.Num != null, "response.Num != null");

            return (int)response.Num.Value;
        }

        private async Task<int> GetRandomComicNumber()
        {
            var maxId = await this.GetLatestComicId();
            var randomNumber = new Random();
            return randomNumber.Next(maxId);
        }

        private async Task<string> GetImageUri(int comicId)
        {
            Comic comicImage = await this.XkcdService.GetComicByIdAsync(comicId).ConfigureAwait(false);

            return comicImage.Img;
        }
    }
}