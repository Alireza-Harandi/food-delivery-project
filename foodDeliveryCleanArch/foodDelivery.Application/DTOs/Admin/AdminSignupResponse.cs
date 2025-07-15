namespace foodDelivery.Application.DTOs.Admin;

public class AdminSignupResponse(string token)
{
    public string Token { get; set; } = token;
}