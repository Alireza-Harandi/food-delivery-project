using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Customer;

public record CustomerOrderDto(
    Guid OrderId,
    Guid RestaurantId,
    OrderStatus OrderStatus,
    double TotalPrice,
    List<OrderItemDto> OrderItems);

public record OrderItemDto(Guid OrderItemId, Guid FoodId, int Quantity);