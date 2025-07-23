namespace foodDelivery.Application.DTOs.Vendor;

public class VendorSignupRequest(string username, string password, string name, string phone)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Name { get; set; } = name;
    public string Phone { get; set; } = phone;
}

public class VendorSignupResponse(string token)
{
    public string Token { get; set; } = token;
}