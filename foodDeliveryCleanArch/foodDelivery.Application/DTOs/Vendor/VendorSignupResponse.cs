namespace foodDelivery.Application.DTOs.Vendor;

public class VendorSignupResponse(string token)
{
    public string Token { get; set; } = token;
}