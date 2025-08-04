using foodDelivery.Application.DTOs.Restaurant;

namespace foodDelivery.Application.DTOs.AuthUser;

public record RestaurantProfileDto(
    Guid RestaurantId,
    string Name,
    string Phone,
    double? Latitude,
    double? Longitude,
    string? Address,
    double Rating,
    List<SetWh> WorkingHours);