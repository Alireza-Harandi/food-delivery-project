namespace foodDelivery.Application.DTOs.Vendor;

public class VendorSignupRequest(string username, string password, string ownerName, string restaurantName, string phoneNumber, string address)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string OwnerName { get; set; } = ownerName;
    public string RestaurantName { get; set; } = restaurantName;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string Address { get; set; } = address;
}