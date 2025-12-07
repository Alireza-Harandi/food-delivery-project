using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record AddToOrderRequest(
    [Required] Guid RestaurantId,
    [Required] Guid FoodId,
    [Required] int Quantity);

public record AddToOrderResponse(Guid OrderId, Guid OrderItemId);