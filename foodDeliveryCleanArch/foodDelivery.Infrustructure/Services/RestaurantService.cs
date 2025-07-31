using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class RestaurantService(DbManager dbManager, IAuthService authService) : IRestaurantService
{
    private async Task CheckAccessAsync(Guid restaurantId)
    {
        Token token = await authService.CheckTokenAsync(Role.Vendor);
        bool hasAccess = await dbManager.Restaurants
            .AnyAsync(r => r.Id == restaurantId && r.Vendor!.UserId == token.UserId);
        if (!hasAccess)
            throw new UnauthorizedAccessException("You do not have access to this restaurant");
    }

    public async Task<AddMenuResponse> AddMenuAsync(AddMenuRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        await CheckAccessAsync(request.RestaurantId);

        bool exists = await dbManager.Menus
            .AnyAsync(m => m.Name == request.Name && m.RestaurantId == request.RestaurantId);
        if (exists)
            throw new ArgumentException("Menu already exists.");

        Menu menu = new Menu(
            request.RestaurantId, request.Category, request.Name
        );
        await dbManager.Menus.AddAsync(menu);
        await dbManager.SaveChangesAsync();

        return new AddMenuResponse(
            menu.RestaurantId,
            menu.Id,
            menu.Category,
            menu.Name
        );
    }

    public async Task<AddFoodResponse> AddFoodAsync(AddFoodRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        await CheckAccessAsync(request.RestaurantId);

        bool exists = await dbManager.Foods
            .AnyAsync(f => f.MenuId == request.MenuId && f.Name == request.Name);
        if (exists)
            throw new ArgumentException("Food already exists.");

        Food food = new Food(
            request.MenuId,
            request.Name,
            request.Price,
            request.Stock,
            request.Description
        );

        await dbManager.Foods.AddAsync(food);
        await dbManager.SaveChangesAsync();

        Menu? menu = await dbManager.Menus.FirstOrDefaultAsync(m => m.Id == request.MenuId);
        if (menu == null)
            throw new KeyNotFoundException("Menu not found.");

        return new AddFoodResponse(
            menu.RestaurantId,
            food.MenuId,
            food.Id,
            food.Name,
            food.Price,
            food.Description,
            food.Stock
        );
    }

    public async Task DeleteMenuAsync(Guid menuId, Guid restaurantId)
    {
        await CheckAccessAsync(restaurantId);

        Menu? menu = await dbManager.Menus.FindAsync(menuId);
        if (menu == null)
            throw new KeyNotFoundException("Menu not found.");

        dbManager.Menus.Remove(menu);
        await dbManager.SaveChangesAsync();
    }

    public async Task DeleteFoodAsync(Guid foodId, Guid restaurantId)
    {
        await CheckAccessAsync(restaurantId);

        Food? food = await dbManager.Foods.FindAsync(foodId);
        if (food == null)
            throw new KeyNotFoundException("Food not found.");

        dbManager.Foods.Remove(food);
        await dbManager.SaveChangesAsync();
    }

    public async Task SetFoodStockAsync(Guid restaurantId, Guid foodId, UpdateStockDto request)
    {
        await CheckAccessAsync(restaurantId);

        Food? food = await dbManager.Foods.FindAsync(foodId);
        if (food == null)
            throw new KeyNotFoundException("Food not found.");

        food.Stock = request.Stock;
        await dbManager.SaveChangesAsync();
    }

    public async Task<SetLocationResponse> SetLocationAsync(SetLocationRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        await CheckAccessAsync(request.RestaurantId);

        Location location = new Location(request.Latitude, request.Longitude, request.Address, request.RestaurantId);
        await dbManager.Locations.AddAsync(location);
        await dbManager.SaveChangesAsync();

        return new SetLocationResponse(
            location.RestaurantId,
            location.Latitude,
            location.Longitude,
            location.Address
        );
    }

    public async Task<SetWhResponse> SetWhAsync(SetWhRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        await CheckAccessAsync(request.RestaurantId);

        var list = request.WhList.Select(
            wh => new WorkingHour(
                wh.Day, TimeOnly.Parse(wh.Start), TimeOnly.Parse(wh.End), request.RestaurantId
            )).ToList();

        await dbManager.WorkingHours.AddRangeAsync(list);
        await dbManager.SaveChangesAsync();

        return new SetWhResponse(
            request.RestaurantId,
            request.WhList
        );
    }

    public async Task<RestaurantOrderDto> GetFinalizedOrdersAsync(Guid restaurantId)
    {
        await CheckAccessAsync(restaurantId);

        Restaurant restaurant = await dbManager.Restaurants
            .Include(r => r.Orders)
            .ThenInclude(o => o.Items)
            .FirstAsync(r => r.Id == restaurantId);

        List<OrderDetailDto> orderDetails = restaurant.Orders
            .Where(o => o.Status == OrderStatus.Finalized)
            .Select(o => new OrderDetailDto(
                o.Id,
                o.RestaurantId,
                o.Status,
                o.Total,
                o.Latitude,
                o.Longitude,
                o.Address,
                o.Items.Select(i => new OrderItemDto(
                    i.Id,
                    i.FoodId,
                    i.Quantity
                )).ToList()
            )).ToList();

        return new RestaurantOrderDto(orderDetails);
    }
}