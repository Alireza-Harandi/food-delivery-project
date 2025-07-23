namespace foodDelivery.Application.DTOs.Vendor;

public class RegisterRestaurantRequest(string name, string phone)
{
    public string Name { get; set; } = name;
    public string Phone { get; set; } = phone;
}

public class RegisterRestaurantResponse(Guid restaurantId, Guid vendorId, string name, string phone)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid VendorId { get; set; } = vendorId;
    public string Name { get; set; } = name;
    public string Phone { get; set; } = phone;
}