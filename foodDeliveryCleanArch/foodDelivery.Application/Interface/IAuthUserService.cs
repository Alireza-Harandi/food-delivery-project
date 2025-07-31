using foodDelivery.Application.DTOs.AuthUser;
using foodDelivery.Application.DTOs.Restaurant;

namespace foodDelivery.Application.Interface;

public interface IAuthUserService
{
    public Task<AutocompleteResponseDto> AutocompleteRestaurantsAsync(string prefix);
    public Task<AutocompleteResponseDto> AutocompleteFoodsAsync(Guid restaurantId, string prefix);
    public Task<MenuDetailsDto> GetMenuAsync(Guid menuId);
    public Task<MenusDto> GetMenusAsync(Guid restaurantId);
    public Task<RestaurantProfileDto> GetRestaurantProfileAsync(Guid restaurantId);
}