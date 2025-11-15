namespace Web;

public class UserAuthApi
{

    private readonly HttpClientHelper _clientHelper;

    public UserAuthApi(HttpClientHelper clientHelper)
    {
        _clientHelper = clientHelper;
    }

    

    public async Task<string> LoginRequest(string url, string content)
    {
        return await _clientHelper.PostAsync(url, content);

    }
    public async Task<string> RegisterRequest(string url, MultipartFormDataContent content)
    {
        return await _clientHelper.PostMultipartFormDataAsync(url, content);

    }

}
