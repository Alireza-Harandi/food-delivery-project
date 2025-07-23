using foodDelivery.Domain;

namespace foodDelivery.Application.DTOs.Restaurant;

public class MenuDetailsDto(Guid menuId, string name, Category menuCategory, List<FoodItemDto> foods)
{
    public Guid MenuId { get; set; } = menuId;
    public string Name { get; set; } = name;
    public Category Category { get; set; } = menuCategory;
    public List<FoodItemDto> Foods { get; set; } = foods;
}