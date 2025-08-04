using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Restaurant;

public record AddFoodRequest(
    [Required] Guid RestaurantId,
    [Required] Guid MenuId,
    [Required] string Name,
    [Required] double Price,
    [Required] string Description,
    [Required] int Stock);

public record AddFoodResponse(
    Guid RestaurantId,
    Guid MenuId,
    Guid FoodId,
    string Name,
    double Price,
    string Description,
    int Stock);