using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public GarfieldComics()
        {
            this.BaseUri = new Uri("https://discordians-api.herokuapp.com/comic");
        }

        private Uri BaseUri { get; }

        private ComicModel ComicModel { get; set; }
        
        public FileResult GetGarfieldComic()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            using (var httpClient = new HttpClient())
            {
                string response = httpClient.GetStringAsync(comicUri).Result;
                this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            }

            byte[] imageBytes;

            using (var response = new WebClient())
            {
                imageBytes = response.DownloadData(this.ComicModel.image);
            }

            var memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/gif");
        }

        public string GetGarfieldComicUri()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            using (var httpClient = new HttpClient())
            {
                string response = httpClient.GetStringAsync(comicUri).Result;
                this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            }

            return this.ComicModel.image;
        }
    }
}