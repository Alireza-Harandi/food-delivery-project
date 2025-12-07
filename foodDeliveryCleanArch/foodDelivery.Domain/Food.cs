using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Food
{
    [Key] public Guid Id { get; set; }
    public Guid MenuId { get; set; }
    [ForeignKey("MenuId")] public Menu? Menu { get; set; }

    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();

    public Food(Guid menuId, string name, double price, int stock, string description)
    {
        Id = Guid.NewGuid();
        MenuId = menuId;
        Name = name;
        Price = price;
        Stock = stock;
        Description = description;
    }

    public Food()
    {
    }
}