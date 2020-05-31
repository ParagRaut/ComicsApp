using System;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics.GarfieldService
{
    public class GarfieldServiceApi
    {
        public string GetGarfieldComicsUrl()
        {
            string dateRange = this.GetRandomDateRange();

            string baseUrl = $"https://d1ejxu6vysztl5.cloudfront.net/comics/garfield/{dateRange}.gif";           

            return baseUrl;
        }

        private string GetRandomDateRange()
        {
            var random = new Random();
            var startDate = new DateTime(1978, 6, 19);
            int dateRange = (DateTime.Today - startDate).Days;
            var timeString = startDate.AddDays(random.Next(dateRange)).ToString("yyyy-MM-dd");
            timeString = $"{timeString.Split("-")[0]}/{timeString}";
            return timeString;
        }      
    }
}
