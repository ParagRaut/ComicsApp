using System;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics.GarfieldService
{
    public class GarfieldServiceApi
    {
        public string GetGarfieldComicsUrl()
        {
            var dateRange = GetRandomDateRange();

            var baseUrl = $"https://d1ejxu6vysztl5.cloudfront.net/comics/garfield/{dateRange}.gif";

            return baseUrl;
        }

        private string GetRandomDateRange()
        {
            Random random = new Random();
            DateTime start = new DateTime(1978, 6, 19);
            int range = (DateTime.Today - start).Days;
            var timeString = start.AddDays(random.Next(range)).ToString("yyyy-MM-dd");
            timeString = $"{timeString.Split("-")[0]}/{timeString}";
            return timeString;
        }
    }
}
