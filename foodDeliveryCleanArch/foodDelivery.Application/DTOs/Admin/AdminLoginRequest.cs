namespace foodDelivery.Application.DTOs.Admin;

public class AdminLoginRequest(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}