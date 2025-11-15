using System.Net.Http.Headers;
using System.Text;

namespace Web;

public class HttpClientHelper
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientHelper(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["ApiLink"]);
        _httpContextAccessor = httpContextAccessor;
    }

    private void SetAuthorizationHeader()
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");


        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
    public async Task<string> GetIdAsync(string url, int id)
    {
        SetAuthorizationHeader();

        var fullUrl = $"{url}/{id}";
        var response = await _httpClient.GetAsync(fullUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostMultipartFormDataAsync(string url, MultipartFormDataContent contentRequest)
    {


        var response = await _httpClient.PostAsync(url, contentRequest);
        if (!response.IsSuccessStatusCode)
        {

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}");
        }
        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> PostAsync(string url, string contentRequest)
    {
        //1
        SetAuthorizationHeader();

        //1
        var content = new StringContent(contentRequest, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}");
        }
        return await response.Content.ReadAsStringAsync();
    }


}
