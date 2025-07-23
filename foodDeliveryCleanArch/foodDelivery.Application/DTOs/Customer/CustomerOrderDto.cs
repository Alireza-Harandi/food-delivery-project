using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Customer;

public class CustomerOrderDto(Guid orderId, Guid restaurantId, OrderStatus orderStatus, double totalPrice, List<OrderItemDto> orderItems)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid RestaurantId { get; set; } = restaurantId;
    public OrderStatus OrderStatus { get; set; } = orderStatus;
    public double TotalPrice { get; set; } = totalPrice;
    public List<OrderItemDto> OrderItems { get; set; } = orderItems;
}

public class OrderItemDto(Guid orderItemId, Guid foodId, int quantity)
{
    public Guid OrderItemId { get; set; }
    public Guid FoodId { get; set; }
    public int Quantity { get; set; }
}