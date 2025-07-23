namespace foodDelivery.Application.DTOs.Customer;

public class SetOrderQuantityDto(Guid orderId, Guid orderItemId, int quantity)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid OrderItemId { get; set; } = orderItemId;
    public int Quantity { get; set; } = quantity;
}