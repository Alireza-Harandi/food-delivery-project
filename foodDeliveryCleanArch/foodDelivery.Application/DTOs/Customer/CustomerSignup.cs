namespace foodDelivery.Application.DTOs.Customer;

public class CustomerSignupRequest(string username, string password, string name, string phoneNumber)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = phoneNumber;
}

public class CustomerSignupResponse(string token)
{
    public string Token { get; set; } = token;
}