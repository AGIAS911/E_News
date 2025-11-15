namespace Anas_Abualsauod.News.Domain.Dtos.User;

public class LoginUserResponse
{
    public int id { get; set; }
    public string accessToken { get; set; }
    public int expiration { get; set; }

    public string userName { get; set; }
}
