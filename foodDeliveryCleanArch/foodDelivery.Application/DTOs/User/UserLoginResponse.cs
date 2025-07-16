namespace foodDelivery.Application.DTOs.User;

public class UserLoginResponse(string token)
{
    public string Token { get; set; } = token;
}