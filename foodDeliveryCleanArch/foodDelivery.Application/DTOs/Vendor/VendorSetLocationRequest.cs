namespace foodDelivery.Application.DTOs.Vendor;

public class VendorSetLocationRequest(double latitude, double longitude, string address)
{
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
}