using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrastructure.Services;

public class CustomerService(DbManager dbManager, IAuthService authService) : ICustomerService
{
    private async Task<Token> CheckAccessAsync()
    {
        Token token = await authService.CheckTokenAsync(Role.Customer);
        if (!await dbManager.Customers.AnyAsync(c => c.UserId == token.UserId))
            throw new UnauthorizedAccessException("customer not found");
        return token;
    }

    public async Task<CustomerSignupResponse> SignupAsync(CustomerSignupRequest request)
    {
        if (await dbManager.Users.AnyAsync(u => u.Username == request.Username))
            throw new ArgumentException("Username already taken");

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.PhoneNumber))
            throw new ArgumentException("Invalid phone number");

        User user = new User(request.Username, request.Password, Role.Customer);
        dbManager.Users.Add(user);
        Customer customer = new Customer(request.Name, request.PhoneNumber, user.Id);
        dbManager.Customers.Add(customer);
        await dbManager.SaveChangesAsync();

        return new CustomerSignupResponse(
            authService.CreateToken(user)
        );
    }

    public async Task<AddToOrderResponse> AddToOrderAsync(AddToOrderRequest request)
    {
        Token token = await CheckAccessAsync();

        Customer? customer = await dbManager.Customers.FirstOrDefaultAsync(c => c.UserId == token.UserId);
        if (customer == null)
            throw new UnauthorizedAccessException("customer not found");

        Order? order = await dbManager.Orders
            .FirstOrDefaultAsync(o =>
                o.CustomerId == customer.Id &&
                o.RestaurantId == request.RestaurantId
            );
        if (order == null)
        {
            order = new Order(
                customer.Id,
                request.RestaurantId
            );
            dbManager.Orders.Add(order);
        }

        OrderItem orderItem = new OrderItem(
            order.Id,
            request.FoodId,
            request.Quantity
        );
        dbManager.OrderItems.Add(orderItem);
        order.Total = await CalculateTotalPriceAsync(order.Id);
        await dbManager.SaveChangesAsync();

        return new AddToOrderResponse(
            order.Id,
            orderItem.Id
        );
    }

    private async Task<double> CalculateTotalPriceAsync(Guid orderId)
    {
        List<OrderItem> orderItems = await dbManager.OrderItems
            .Include(o => o.Food)
            .Where(o => o.OrderId == orderId)
            .ToListAsync();

        double total = 0;
        foreach (var orderItem in orderItems)
        {
            total += orderItem.Food!.Price * orderItem.Quantity;
        }

        return total;
    }

    public async Task SetOrderQuantityAsync(SetOrderQuantityDto request)
    {
        Token token = await CheckAccessAsync();
        Customer? customer = await dbManager.Customers.FirstOrDefaultAsync(c => c.UserId == token.UserId);
        if (customer == null)
            throw new UnauthorizedAccessException("customer not found");

        OrderItem? orderItem = await dbManager.OrderItems
            .Include(i => i.Order)
            .FirstOrDefaultAsync(i =>
                i.Id == request.OrderItemId);

        Order? order = await dbManager.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (orderItem == null)
            throw new KeyNotFoundException("OrderItem not found");
        if (orderItem.Order!.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer has no access");

        if (request.Quantity == 0)
        {
            dbManager.OrderItems.Remove(orderItem);
            if (order.Items.Count == 0)
                dbManager.Orders.Remove(order);
        }
        else
        {
            orderItem.Quantity = request.Quantity;
            order.Total = await CalculateTotalPriceAsync(order.Id);
        }

        await dbManager.SaveChangesAsync();
    }

    public async Task<CustomerOrderDto> GetOrdersAsync(Guid orderId)
    {
        Token token = await CheckAccessAsync();
        Customer? customer = await dbManager.Customers.FirstOrDefaultAsync(c => c.UserId == token.UserId);
        if (customer == null)
            throw new UnauthorizedAccessException("customer not found");

        Order? order = await dbManager.Orders
            .Include(o => o.Items)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer has no access");

        return new CustomerOrderDto(
            order.Id,
            order.RestaurantId,
            order.Status,
            order.Total,
            order.Items.Select(i => new OrderItemDto(
                i.Id,
                i.FoodId,
                i.Quantity
            )).ToList()
        );
    }

    public async Task<FinalizeOrderResponse> FinalizeOrderAsync(FinalizeOrderRequest request)
    {
        Token token = await CheckAccessAsync();
        Customer customer = await dbManager.Customers.FirstAsync(c => c.UserId == token.UserId);

        Order? order = await dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer has no access");

        if (!await IsFoodsAvailableAsync(order.Items))
            throw new InvalidOperationException("Insufficient quantity for one or more food items in the order");
        order.Longitude = request.Longitude;
        order.Latitude = request.Latitude;
        order.Address = request.Address;
        order.Status = OrderStatus.Finalized;
        order.CreatedAt = DateTime.UtcNow;
        await dbManager.SaveChangesAsync();

        return new FinalizeOrderResponse(
            order.Id
        );
    }

    private async Task<bool> IsFoodsAvailableAsync(List<OrderItem> items)
    {
        var foodIds = items.Select(i => i.FoodId).ToList();
        var foods = await dbManager.Foods.Where(f => foodIds.Contains(f.Id)).ToListAsync();

        foreach (var item in items)
        {
            var food = foods.FirstOrDefault(f => f.Id == item.FoodId);
            if (food == null || food.Stock < item.Quantity)
            {
                return false;
            }
        }

        foreach (var item in items)
        {
            var food = foods.First(f => f.Id == item.FoodId);
            food.Stock -= item.Quantity;
        }

        await dbManager.SaveChangesAsync();
        return true;
    }

    public async Task ReportRestaurantAsync(ReportRestaurantDto request)
    {
        Token token = await CheckAccessAsync();

        Customer customer = await dbManager.Customers.FirstAsync(c => c.UserId == token.UserId);
        Report report = new Report(
            request.RestaurantId,
            customer.Id,
            request.Description
        );

        dbManager.Reports.Add(report);
        await dbManager.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        Token token = await CheckAccessAsync();
        Order? order = await dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.Customer!.UserId != token.UserId)
            throw new UnauthorizedAccessException("Customer has no access");

        dbManager.Orders.Remove(order);
        await dbManager.SaveChangesAsync();
    }

    public async Task SubmitRatingAsync(SubmitRatingDto request)
    {
        Token token = await CheckAccessAsync();
        Order? order = await dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Restaurant)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);
        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.Customer!.UserId != token.UserId)
            throw new UnauthorizedAccessException("Customer has no access");

        order.Restaurant!.RatingSum += request.Score;
        order.Restaurant.RatingCount++;
        order.Restaurant.Rating = order.Restaurant.RatingSum / order.Restaurant.RatingCount;

        dbManager.Orders.Remove(order);
        await dbManager.SaveChangesAsync();
    }

    public async Task<CustomerProfileDto> GetProfileAsync()
    {
        Token token = await CheckAccessAsync();
        Customer customer = await dbManager.Customers.FirstAsync(c => c.UserId == token.UserId);

        return new CustomerProfileDto(
            customer.Id,
            customer.Name,
            customer.PhoneNumber
        );
    }
}