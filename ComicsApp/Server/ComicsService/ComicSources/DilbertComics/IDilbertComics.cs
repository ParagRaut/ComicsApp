using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.ComicsService.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        FileResult GetDilbertComic();
        string GetDilbertComicUri();
    }
}