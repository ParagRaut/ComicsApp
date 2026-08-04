using Refit;

namespace ComicsProvider.Imgflip;

public interface IImgflipApi
{
    [Get("/get_memes")]
    Task<ImgflipResponse> GetMemes();
}

public sealed class ImgflipService
{
    private readonly IImgflipApi _imgflipApi;

    public ImgflipService(IImgflipApi imgflipApi)
    {
        _imgflipApi = imgflipApi;
    }

    public async Task<string> GetRandomImageAsync()
    {
        var response = await _imgflipApi.GetMemes();
        var memes = response.data?.memes;

        return memes is null || memes.Count == 0
            ? string.Empty
            : memes[Random.Shared.Next(memes.Count)].url;
    }
}

public record ImgflipResponse(bool success, ImgflipData data);

public record ImgflipData(List<ImgflipMeme> memes);

public record ImgflipMeme(string id, string name, string url);
