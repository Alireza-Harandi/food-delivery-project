namespace foodDelivery.Application.DTOs.Admin;

public class AdminLoginResponse(string token)
{
    public string Token { get; set; } = token;
}