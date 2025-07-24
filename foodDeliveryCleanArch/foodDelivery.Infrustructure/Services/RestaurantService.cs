using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.DTOs.Restaurant;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class RestaurantService(DbManager dbManager, IAuthService authService) : IRestaurantService
{
    public Token CheckAccess(Guid restaurantId)
    {
        Token token = authService.CheckToken(Role.Vendor);
        if (!dbManager.Restaurants.Any(r => r.Id == restaurantId && r.Vendor!.UserId == token.UserId))
            throw new UnauthorizedAccessException("You do not have access to this restaurant");
        return token;
    }

    public AddMenuResponse AddMenu(AddMenuRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        CheckAccess(request.RestaurantId);
        if (dbManager.Menus.Any(m => m.Name == request.Name && m.RestaurantId == request.RestaurantId))
            throw new ArgumentException("Menu already exists.");

        Menu menu = new Menu(
            request.RestaurantId, request.Category, request.Name
        );
        dbManager.Menus.Add(menu);
        dbManager.SaveChanges();

        return new AddMenuResponse(
            menu.RestaurantId,
            menu.Id,
            menu.Category,
            menu.Name
        );
    }

    public AddFoodResponse AddFood(AddFoodRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        CheckAccess(request.RestaurantId);
        if (dbManager.Foods.Any(f => f.MenuId == request.MenuId && f.Name == request.Name))
            throw new ArgumentException("Food already exists.");

        Food food = new Food(
            request.MenuId,
            request.Name,
            request.Price,
            request.Stock,
            request.Description
        );

        dbManager.Foods.Add(food);
        dbManager.SaveChanges();

        Menu? menu = dbManager.Menus.FirstOrDefault(m => m.Id == request.MenuId);
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

    public void DeleteMenu(Guid menuId, Guid restaurantId)
    {
        CheckAccess(restaurantId);
        Menu? menu = dbManager.Menus.Find(menuId);
        if (menu == null)
            throw new KeyNotFoundException("Menu not found.");
        dbManager.Menus.Remove(menu);
        dbManager.SaveChanges();
    }

    public void DeleteFood(Guid foodId, Guid restaurantId)
    {
        CheckAccess(restaurantId);
        Food? food = dbManager.Foods.Find(foodId);
        if (food == null)
            throw new KeyNotFoundException("Food not found.");
        dbManager.Foods.Remove(food);
        dbManager.SaveChanges();
    }

    public void SetFoodStock(Guid restaurantId, Guid foodId, UpdateStockDto request)
    {
        CheckAccess(restaurantId);
        Food? food = dbManager.Foods.Find(foodId);
        if (food == null)
            throw new KeyNotFoundException("Food not found.");
        food.Stock = request.Stock;
        dbManager.SaveChanges();
    }

    public SetLocationResponse SetLocation(SetLocationRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        CheckAccess(request.RestaurantId);

        Location location = new Location(request.Latitude, request.Longitude, request.Address, request.RestaurantId);
        dbManager.Locations.Add(location);
        dbManager.SaveChanges();

        return new SetLocationResponse(
            location.RestaurantId,
            location.Latitude,
            location.Longitude,
            location.Address
        );
    }

    public SetWhResponse SetWh(SetWhRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        CheckAccess(request.RestaurantId);

        var list = request.WhList.Select(
            wh => new WorkingHour(
                wh.Day, TimeOnly.Parse(wh.Start), TimeOnly.Parse(wh.End), request.RestaurantId
            )).ToList();
        dbManager.WorkingHours.AddRange(list);
        dbManager.SaveChanges();

        return new SetWhResponse(
            request.RestaurantId,
            request.WhList
        );
    }

    public RestaurantOrderDto GetFinalizedOrders(Guid restaurantId)
    {
        CheckAccess(restaurantId);
        Restaurant restaurant = dbManager.Restaurants
            .Include(r => r.Orders)
            .ThenInclude(o => o.Items)
            .First(r => r.Id == restaurantId);
        
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