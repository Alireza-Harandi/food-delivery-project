namespace foodDelivery.Application.DTOs.Restaurant;

public record FoodItemDto(
    Guid FoodId,
    string Name,
    int Stock,
    double Price,
    string Description);