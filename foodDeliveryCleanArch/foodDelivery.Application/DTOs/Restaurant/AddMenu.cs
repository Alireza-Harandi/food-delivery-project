using System.ComponentModel.DataAnnotations;
using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public record AddMenuRequest(
    [Required] Guid RestaurantId,
    [Required] Category Category,
    [Required] string Name);

public record AddMenuResponse(
    Guid RestaurantId,
    Guid MenuId,
    Category Category,
    string Name);