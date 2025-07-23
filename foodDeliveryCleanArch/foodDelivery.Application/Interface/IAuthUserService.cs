using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.DTOs.Restaurant;

namespace foodDelivery.Application.Interface;

public interface IAuthUserService
{
    public AutocompleteResponseDto AutocompleteRestaurants(string prefix);
    public MenuDetailsDto GetMenu(Guid menuId);
    public MenusDto GetMenus(Guid restaurantId);
}