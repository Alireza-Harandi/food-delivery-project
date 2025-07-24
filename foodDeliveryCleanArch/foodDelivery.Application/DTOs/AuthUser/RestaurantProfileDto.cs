using foodDelivery.Application.DTOs.Restaurant;

namespace foodDelivery.Application.DTOs.AuthUser;

public class RestaurantProfileDto(
    Guid restaurantId,
    string name,
    string phone,
    double? latitude,
    double? longitude,
    string? address,
    double rating,
    List<SetWh> workingHours)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public string Name { get; set; } = name;
    public string Phone { get; set; } = phone;
    public double? Latitude { get; set; } = latitude;
    public double? Longitude { get; set; } = longitude;
    public string? Address { get; set; } = address;
    public double Rating { get; set; } = rating;
    public List<SetWh> WorkingHours { get; set; } = workingHours;
}