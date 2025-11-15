namespace Web;

public class UserApi
{
    private readonly HttpClientHelper _clientHelper;

    public UserApi(HttpClientHelper clientHelper)
    {
        _clientHelper = clientHelper;
    }

    public async Task<string> GetUserById(string url,int id)
    {
        return await _clientHelper.GetIdAsync(url, id);
    }


}
