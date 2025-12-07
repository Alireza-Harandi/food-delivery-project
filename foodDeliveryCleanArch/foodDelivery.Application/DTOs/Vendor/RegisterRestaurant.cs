using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Vendor;

public record RegisterRestaurantRequest(
    [Required] string Name,
    [Required] string Phone);

public record RegisterRestaurantResponse(
    Guid RestaurantId,
    Guid VendorId,
    string Name,
    string Phone);