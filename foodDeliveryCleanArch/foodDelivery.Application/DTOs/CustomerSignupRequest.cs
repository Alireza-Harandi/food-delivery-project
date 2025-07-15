namespace foodDelivery.Application.DTOs;

public class CustomerSignupRequest(string username, string password, string name, string phoneNumber)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = phoneNumber;
}