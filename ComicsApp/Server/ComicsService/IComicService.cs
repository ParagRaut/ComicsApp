using Microsoft.AspNetCore.Mvc;

namespace ComicsApp.Server.ComicsService
{
    public interface IComicService
    {
        FileResult GetRandomComic();
    }
}