using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<string> GetGarfieldComicUri()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            var httpClient = new HttpClient();

            string response = await httpClient.GetStringAsync(comicUri);
            this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            
            return this.ComicModel.image;
        }
    }
}
