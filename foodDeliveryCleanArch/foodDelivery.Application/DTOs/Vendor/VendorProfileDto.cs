namespace foodDelivery.Application.DTOs.Vendor;

public record VendorProfileDto(
    Guid VendorId,
    string Name,
    string Phone,
    List<RestaurantDetail> Restaurants);

public record RestaurantDetail(Guid RestaurantId, string Name);