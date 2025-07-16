namespace foodDelivery.Application.DTOs.Vendor;

public class VendorSetLocationResponse(Guid vendorId, double latitude, double longitude, string address)
{
    public Guid VendorId { get; set; } = vendorId;
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
}