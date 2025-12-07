using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Order
{
    [Key] public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    [ForeignKey("CustomerId")] public Customer? Customer { get; set; }

    public Guid RestaurantId { get; set; }
    [ForeignKey("RestaurantId")] public Restaurant? Restaurant { get; set; }

    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public double Total { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; }

    public Order(Guid customerId, Guid restaurantId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Reserved;
        Total = 0;
        Latitude = 0;
        Longitude = 0;
        Address = string.Empty;
    }

    public Order()
    {
    }
}