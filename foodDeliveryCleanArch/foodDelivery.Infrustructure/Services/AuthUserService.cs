using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class AuthUserService(DbManager dbManager, IAuthService authService) : IAuthUserService
{
    private Token CheckAccess()
    {
        Token token = authService.CheckToken();
        if (!dbManager.Users.Any(u => u.Id == token.UserId))
            throw new UnauthorizedAccessException("user not found");
        return token;
    }
    
    public AutocompleteResponseDto AutocompleteRestaurants(string prefix)
    {
        CheckAccess();
        if (string.IsNullOrWhiteSpace(prefix))
            throw new AggregateException("Prefix is required.");

        List<AutocompleteItemDto> restaurants = dbManager.Restaurants
            .Where(r => r.Name.StartsWith(prefix))
            .OrderBy(r => r.Rating)
            .Select(r => new AutocompleteItemDto(
                r.Id,
                r.Name
            ))
            .Take(5)
            .ToList();
        return new AutocompleteResponseDto(restaurants);
    }
    
    public MenuDetailsDto GetMenu(Guid menuId)
    {
        CheckAccess();
        Menu? menu = dbManager.Menus
            .Include(m => m.Foods)
            .FirstOrDefault(m => m.Id == menuId);
        if (menu == null)
            throw new KeyNotFoundException("Menu not found.");
        return new MenuDetailsDto(
            menu.Id,
            menu.Name,
            menu.Category,
            menu.Foods.Select(f => new FoodItemDto(f.Id, f.Name, f.Stock, f.Price, f.Description)).ToList()
        );
    }
    
    public MenusDto GetMenus(Guid restaurantId)
    {
        CheckAccess();
        List<MenuDetailsDto> menus = dbManager.Menus
            .Include(m => m.Foods)
            .Where(m => m.RestaurantId == restaurantId)
            .Select(m => new MenuDetailsDto(
                m.Id,
                m.Name,
                m.Category,
                m.Foods.Select(f => new FoodItemDto(
                    f.Id,
                    f.Name,
                    f.Stock,
                    f.Price,
                    f.Description
                )).ToList()
            )).ToList();
        return new MenusDto(menus);
    }
}