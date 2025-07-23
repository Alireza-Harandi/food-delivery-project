namespace foodDelivery.Application.DTOs.Restaurant;

public class FoodItemDto(Guid foodId, string name, int stock, double price, string description)
{
    public Guid FoodId { get; set; } = foodId;
    public string Name { get; set; } = name;
    public int Stock { get; set; } = stock;
    public double Price { get; set; } = price;
    public string Description { get; set; } = description;
}