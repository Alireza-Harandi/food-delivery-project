namespace foodDelivery.Application.DTOs.Customer;

public class CustomerLoginRequest(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}