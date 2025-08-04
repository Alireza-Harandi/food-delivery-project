using foodDelivery.Application.DTOs.AuthUser;
using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrastructure.Services;

public class AuthUserService(DbManager dbManager, IAuthService authService) : IAuthUserService
{
    private async Task CheckAccessAsync()
    {
        Token token = await authService.CheckTokenAsync();
        bool exists = await dbManager.Users.AnyAsync(u => u.Id == token.UserId);
        if (!exists)
            throw new UnauthorizedAccessException("user not found");
    }

    public async Task<AutocompleteResponseDto> AutocompleteRestaurantsAsync(string prefix)
    {
        await CheckAccessAsync();

        if (string.IsNullOrWhiteSpace(prefix))
            throw new ArgumentException("Prefix is required.");

        List<AutocompleteItemDto> restaurants = await dbManager.Restaurants
            .Where(r => r.Name.StartsWith(prefix))
            .OrderBy(r => r.Rating)
            .Select(r => new AutocompleteItemDto(
                r.Id,
                r.Name
            ))
            .Take(5)
            .ToListAsync();

        return new AutocompleteResponseDto(restaurants);
    }

    public async Task<AutocompleteResponseDto> AutocompleteFoodsAsync(Guid restaurantId, string prefix)
    {
        await CheckAccessAsync();

        if (string.IsNullOrWhiteSpace(prefix))
            throw new ArgumentException("Prefix is required.");

        List<AutocompleteItemDto> foods = await dbManager.Foods
            .Where(f => f.Menu!.RestaurantId == restaurantId && f.Name.StartsWith(prefix))
            .Select(f => new AutocompleteItemDto(
                f.Id,
                f.Name
            ))
            .Take(5)
            .ToListAsync();

        return new AutocompleteResponseDto(foods);
    }

    public async Task<MenuDetailsDto> GetMenuAsync(Guid menuId)
    {
        await CheckAccessAsync();

        Menu? menu = await dbManager.Menus
            .Include(m => m.Foods)
            .FirstOrDefaultAsync(m => m.Id == menuId);

        if (menu == null)
            throw new KeyNotFoundException("Menu not found.");

        return new MenuDetailsDto(
            menu.Id,
            menu.Name,
            menu.Category,
            menu.Foods.Select(f => new FoodItemDto(f.Id, f.Name, f.Stock, f.Price, f.Description)).ToList()
        );
    }

    public async Task<MenusDto> GetMenusAsync(Guid restaurantId)
    {
        await CheckAccessAsync();

        List<MenuDetailsDto> menus = await dbManager.Menus
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
            )).ToListAsync();

        return new MenusDto(menus);
    }

    public async Task<RestaurantProfileDto> GetRestaurantProfileAsync(Guid restaurantId)
    {
        await CheckAccessAsync();

        Restaurant? restaurant = await dbManager.Restaurants
            .Include(r => r.Location)
            .Include(r => r.WorkingHours)
            .FirstOrDefaultAsync(r => r.Id == restaurantId);

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