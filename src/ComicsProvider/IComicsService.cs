namespace ComicsProvider;

public interface IComicsService
{
    Task<string> GetGarfieldComics();

    Task<string> GetCalvinAndHobbesComics();
    
    Task<string> GetXkcdComics();
}
