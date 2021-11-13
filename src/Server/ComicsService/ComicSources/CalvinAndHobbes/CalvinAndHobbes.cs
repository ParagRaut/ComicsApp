namespace ComicsApp.Server.ComicsService.ComicSources.CalvinAndHobbes;

public class CalvinAndHobbes : ICalvinAndHobbes
{
    public async Task<string> CalvinAndHobbesComicUri()
    {
        return await Service.GetComicUri();
    }
}
