using foodDelivery.Application.DTOs.AuthUser;
using foodDelivery.Application.DTOs.Restaurant;

namespace foodDelivery.Application.Interface;

public interface IAuthUserService
{
    public AutocompleteResponseDto AutocompleteRestaurants(string prefix);
    public AutocompleteResponseDto AutocompleteFoods(Guid restaurantId, string prefix);
    public MenuDetailsDto GetMenu(Guid menuId);
    public MenusDto GetMenus(Guid restaurantId);
}