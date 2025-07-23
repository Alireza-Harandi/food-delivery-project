namespace foodDelivery.Application.DTOs.Customer;

public class AddToOrderRequest(Guid restaurantId, Guid foodId, int quantity)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid FoodId { get; set; } = foodId;
    public int Quantity { get; set; } = quantity;
}

public class AddToOrderResponse(Guid orderId, Guid orderItemId)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid OrderItemId { get; set; } = orderItemId;
}


