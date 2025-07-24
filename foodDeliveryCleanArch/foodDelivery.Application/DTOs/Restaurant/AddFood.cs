namespace foodDelivery.Application.DTOs.Restaurant;

public class AddFoodRequest(Guid restaurantId, Guid menuId, string name, double price, string description, int stock)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid MenuId { get; set; } = menuId;
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;
    public int Stock { get; set; } = stock;
    public string Description { get; set; } = description;
}

public class AddFoodResponse(
    Guid restaurantId,
    Guid menuId,
    Guid foodId,
    string name,
    double price,
    string description,
    int stock)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public Guid MenuId { get; set; } = menuId;
    public Guid FoodId { get; set; } = foodId;
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;
    public int Stock { get; set; } = stock;
    public string Description { get; set; } = description;
}