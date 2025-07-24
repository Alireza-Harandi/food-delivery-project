using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class CustomerService(DbManager dbManager, IAuthService authService) : ICustomerService
{
    private Token CheckAccess()
    {
        Token token = authService.CheckToken(Role.Customer);
        if (!dbManager.Customers.Any(c => c.UserId == token.UserId))
            throw new UnauthorizedAccessException("customer not found");
        return token;
    }

    public CustomerSignupResponse Signup(CustomerSignupRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username))
            throw new ArgumentException("Username already taken");

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.PhoneNumber))
            throw new ArgumentException("Invalid phone number");

        User user = new User(request.Username, request.Password, Role.Customer);
        dbManager.Users.Add(user);
        Customer customer = new Customer(request.Name, request.PhoneNumber, user.Id);
        dbManager.Customers.Add(customer);
        dbManager.SaveChanges();

        return new CustomerSignupResponse(
            authService.CreateToken(user)
        );
    }

    public AddToOrderResponse AddToOrder(AddToOrderRequest request)
    {
        Token token = CheckAccess();
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        Customer customer = dbManager.Customers.FirstOrDefault(c => c.UserId == token.UserId)!;

        Order? order = dbManager.Orders
            .FirstOrDefault(o =>
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
        order.Total = CalculateTotalPrice(order.Id);
        dbManager.SaveChanges();

        return new AddToOrderResponse(
            order.Id,
            orderItem.Id
        );
    }

    private double CalculateTotalPrice(Guid orderId)
    {
        List<OrderItem> orderItems = dbManager.OrderItems
            .Include(o => o.Food)
            .Where(o => o.OrderId == orderId)
            .ToList();

        double total = 0;
        foreach (var orderItem in orderItems)
        {
            total += orderItem.Food!.Price * orderItem.Quantity;
        }

        return total;
    }

    public void SetOrderQuantity(SetOrderQuantityDto request)
    {
        Token token = CheckAccess();
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        Customer customer = dbManager.Customers.FirstOrDefault(c => c.UserId == token.UserId)!;

        OrderItem? orderItem = dbManager.OrderItems
            .Include(i => i.Order)
            .FirstOrDefault(i =>
                i.Id == request.OrderItemId);

        Order? order = dbManager.Orders
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == request.OrderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (orderItem == null)
            throw new KeyNotFoundException("OrderItem not found");
        if (orderItem.Order!.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer is not access");

        if (request.Quantity == 0)
        {
            dbManager.OrderItems.Remove(orderItem);
            if (order.Items.Count == 0)
                dbManager.Orders.Remove(order);
        }
        else
        {
            orderItem.Quantity = request.Quantity;
            order.Total = CalculateTotalPrice(order.Id);
        }

        dbManager.SaveChanges();
    }

    public CustomerOrderDto GetOrders(Guid orderId)
    {
        Token token = CheckAccess();
        Customer customer = dbManager.Customers.FirstOrDefault(c => c.UserId == token.UserId)!;

        Order? order = dbManager.Orders
            .Include(o => o.Items)
            .Include(o => o.Customer)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer is not access");

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

    public FinalizeOrderResponse FinalizeOrder(FinalizeOrderRequest request)
    {
        Token token = CheckAccess();
        Customer customer = dbManager.Customers.First(c => c.UserId == token.UserId);

        Order? order = dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == request.OrderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != customer.Id)
            throw new UnauthorizedAccessException("Customer is not access");

        if (!IsFoodsAvailable(order.Items))
            throw new InvalidOperationException("Insufficient quantity for one or more food items in the order");
        order.Longitude = request.Longitude;
        order.Latitude = request.Latitude;
        order.Address = request.Address;
        order.Status = OrderStatus.Finalized;
        order.CreatedAt = DateTime.UtcNow;
        dbManager.SaveChanges();

        return new FinalizeOrderResponse(
            order.Id
        );
    }

    private bool IsFoodsAvailable(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            Food? food = dbManager.Foods.FirstOrDefault(f => f.Id == item.FoodId);
            if (food == null || food.Stock < item.Quantity)
            {
                return false;
            }
        }

        foreach (var item in items)
        {
            Food food = dbManager.Foods.First(f => f.Id == item.FoodId);
            food.Stock -= item.Quantity;
        }

        dbManager.SaveChanges();
        return true;
    }

    public void ReportRestaurant(ReportRestaurantDto request)
    {
        Token token = CheckAccess();
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        
        Customer customer = dbManager.Customers.First(c => c.UserId == token.UserId);
        Report report = new Report(
            request.RestaurantId,
            customer.Id,
            request.Description
        );
        
        dbManager.Reports.Add(report);
        dbManager.SaveChanges();
    }

    public void DeleteOrder(Guid orderId)
    {
        Token token = CheckAccess();
        Order? order = dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != token.UserId)
            throw new UnauthorizedAccessException("Customer is not access");
        
        dbManager.Orders.Remove(order);
        dbManager.SaveChanges();
    }

    public void SubmitRating(SubmitRatingDto request)
    {
        Token token = CheckAccess();
        Order? order = dbManager.Orders
            .Include(o => o.Customer)
            .Include(o => o.Restaurant)
            .FirstOrDefault(o => o.Id == request.OrderId);
        if (order == null)
            throw new KeyNotFoundException("Order not found");
        if (order.CustomerId != token.UserId)
            throw new UnauthorizedAccessException("Customer is not access");
        
        order.Restaurant!.RatingSum += request.Score;
        order.Restaurant.RatingCount++;
        order.Restaurant.Rating = order.Restaurant.RatingSum / order.Restaurant.RatingCount;
        
        dbManager.Orders.Remove(order);
        dbManager.SaveChanges();
    }
}