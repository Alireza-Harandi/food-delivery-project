using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public record MenuDetailsDto(
    Guid MenuId,
    string Name,
    Category MenuCategory,
    List<FoodItemDto> Foods);