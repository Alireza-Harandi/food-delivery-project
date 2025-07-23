using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    public Guid FoodId { get; set; }
    [ForeignKey("FoodId")]
    public Food? Food { get; set; }
    public int Quantity { get; set; }

    public OrderItem(Guid orderId, Guid foodId, int quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        FoodId = foodId;
        Quantity = quantity;
    }

    public OrderItem()
    {
    }
}