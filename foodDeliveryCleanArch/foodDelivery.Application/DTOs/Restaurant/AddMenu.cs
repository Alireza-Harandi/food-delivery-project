using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public class AddMenuRequest(Guid restaurantId, Category category, string name)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Category Category { get; set; } = category;
    public string Name { get; set; } = name;
}

public class AddMenuResponse(Guid restaurantId, Guid menuId, Category category, string name)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid MenuId { get; set; } = menuId;
    public Category Category { get; set; } = category;
    public string Name { get; set; } = name;
}