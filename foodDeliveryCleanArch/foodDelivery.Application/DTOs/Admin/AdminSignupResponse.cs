namespace foodDelivery.Application.DTOs.Admin;

public class AdminSignupResponse(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}