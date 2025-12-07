using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IRestaurantService
{
    public Task<SetLocationResponse> SetLocationAsync(SetLocationRequest request);
    public Task<SetWhResponse> SetWhAsync(SetWhRequest request);
    public Task<AddMenuResponse> AddMenuAsync(AddMenuRequest request);
    public Task<AddFoodResponse> AddFoodAsync(AddFoodRequest request);
    public Task DeleteMenuAsync(Guid menuId, Guid restaurantId);
    public Task DeleteFoodAsync(Guid foodId, Guid restaurantId);
    public Task SetFoodStockAsync(Guid restaurantId, Guid foodId, UpdateStockDto request);
    public Task<RestaurantOrderDto> GetFinalizedOrdersAsync(Guid restaurantId);
}