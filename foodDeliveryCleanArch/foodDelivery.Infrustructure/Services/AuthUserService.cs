using foodDelivery.Application.DTOs.AuthUser;
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
            throw new ArgumentException("Prefix is required.");

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

    public AutocompleteResponseDto AutocompleteFoods(Guid restaurantId, string prefix)
    {
        CheckAccess();
        if (string.IsNullOrWhiteSpace(prefix))
            throw new ArgumentException("Prefix is required.");

        List<AutocompleteItemDto> foods = dbManager.Foods
            .Where(f => f.Menu!.RestaurantId == restaurantId && f.Name.StartsWith(prefix))
            .Select(f => new AutocompleteItemDto(
                f.Id,
                f.Name
            ))
            .Take(5)
            .ToList();
        return new AutocompleteResponseDto(foods);
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

    public RestaurantProfileDto GetRestaurantProfile(Guid restaurantId)
    {
        CheckAccess();
        Restaurant? restaurant = dbManager.Restaurants
            .Include(r => r.Location)
            .Include(r => r.WorkingHours)
            .FirstOrDefault(r => r.Id == restaurantId);
        if (restaurant == null)
            throw new KeyNotFoundException("Restaurant not found.");

        return new RestaurantProfileDto(
            restaurant.Id,
            restaurant.Name,
            restaurant.Phone,
            restaurant.Location?.Latitude,
            restaurant.Location?.Longitude,
            restaurant.Location?.Address,
            restaurant.Rating,
            restaurant.WorkingHours.Select(w => new SetWh(
                w.Day,
                w.Start.ToString(),
                w.End.ToString()
            )).ToList()
        );
    }
}