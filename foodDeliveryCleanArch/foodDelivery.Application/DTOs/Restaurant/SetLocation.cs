using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Restaurant;

public record SetLocationRequest(
    [Required] double Latitude,
    [Required] double Longitude,
    [Required] string Address,
    [Required] Guid RestaurantId);

public record SetLocationResponse(
    Guid RestaurantId,
    double Latitude,
    double Longitude,
    string Address);