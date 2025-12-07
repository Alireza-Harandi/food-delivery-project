using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public record RestaurantOrderDto(List<OrderDetailDto> OrderDetails);

public record OrderDetailDto(
    Guid OrderId,
    Guid RestaurantId,
    OrderStatus OrderStatus,
    double TotalPrice,
    double Latitude,
    double Longitude,
    string Address,
    List<OrderItemDto> OrderItems);