using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

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
        if (dbManager.Users.Any(u => u.Username == request.Username) )
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
}