namespace foodDelivery.Application.DTOs;

public class CustomerLoginRequest(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}