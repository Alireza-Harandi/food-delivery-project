namespace foodDelivery.Application.DTOs.Vendor;

public class VendorProfileDto(Guid vendorId, string name, string phone, List<RestaurantDetail> restaurants)
{
    public Guid VendorId { get; set; } = vendorId;
    public string Name { get; set; } = name;
    public string Phone { get; set; } = phone;
    public List<RestaurantDetail> Restaurants { get; set; } = restaurants;
}

public class RestaurantDetail(Guid restaurantId, string name)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public string Name { get; set; } = name;
}