namespace foodDelivery.Application.DTOs.Vendor;

public class VendorProfileDto(Guid vendorId, string name, string phone, List<RestaurantDetail> restaurants)
{
    public Guid VendorId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public List<RestaurantDetail> Restaurants { get; set; } = restaurants;
}

public class RestaurantDetail(Guid restaurantId, string name)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public string Name { get; set; } = name;
}