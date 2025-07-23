using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IRestaurantService
{
    public SetLocationResponse SetLocation(SetLocationRequest request);
    public SetWhResponse SetWh(SetWhRequest request);
    public Token CheckAccess(Guid restaurantId); 
    public AddMenuResponse AddMenu(AddMenuRequest request);
    public AddFoodResponse AddFood(AddFoodRequest request);
    public void DeleteMenu(Guid menuId, Guid restaurantId);
    public void DeleteFood(Guid foodId, Guid restaurantId);
    public MenuDetailsDto GetMenu(Guid restaurantId, Guid menuId);
    public void SetFoodStock(Guid restaurantId, Guid foodId, UpdateStockDto request);
    public MenusDto GetMenus(Guid restaurantId);
}