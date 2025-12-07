using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Report
{
    [Key] public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    [ForeignKey("CustomerId")] public Customer? Customer { get; set; }

    public Guid RestaurantId { get; set; }
    [ForeignKey("RestaurantId")] public Restaurant? Restaurant { get; set; }

    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Report(Guid restaurantId, Guid customerId, string description)
    {
        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        CustomerId = customerId;
        Date = DateTime.UtcNow;
        Description = description;
    }
}