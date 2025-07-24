using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public class RestaurantOrderDto(List<OrderDetailDto> orderDetails)
{
    public List<OrderDetailDto> OrderDetails = orderDetails;
}

public class OrderDetailDto(Guid orderId, Guid restaurantId, OrderStatus orderStatus, double totalPrice, double latitude, double longitude, string address, List<OrderItemDto> orderItems)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid RestaurantId { get; set; } = restaurantId;
    public OrderStatus OrderStatus { get; set; } = orderStatus;
    public double TotalPrice { get; set; } = totalPrice;
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
    public List<OrderItemDto> OrderItems { get; set; } = orderItems;
}