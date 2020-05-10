using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.GarfieldComics
{
    public interface IGarfieldComics
    {
        Task<string> GetGarfieldComicUri();
    }
}