﻿using System.Threading.Tasks;

namespace ComicsApp.Server.ComicsService.ComicSources.Garfield
{
    public interface IGarfield
    {
        Task<string> GetGarfieldComicUri();
    }
}