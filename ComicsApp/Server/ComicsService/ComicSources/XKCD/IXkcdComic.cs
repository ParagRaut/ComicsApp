using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.ComicsService.ComicSources.XKCD
{
    public interface IXkcdComic
    {
        FileResult GetXkcdComic();
        string GetXkcdComicUri();
    }
}