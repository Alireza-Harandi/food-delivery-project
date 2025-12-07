using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Menu
{
    [Key] public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    [ForeignKey("RestaurantId")] public Restaurant? Restaurant { get; set; }

    public Category Category { get; set; }
    public string Name { get; set; }
    public List<Food> Foods { get; set; } = new();

    public Menu(Guid restaurantId, Category category, string name)
    {
        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        Category = category;
        Name = name;
    }

    public Menu()
    {
    }
}