using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

namespace foodDelivery.Infrustructure.Services;

public class CustomerService(DbManager dbManager, IAuthService authService) : ICustomerService
{
    public CustomerSignupResponse Signup(CustomerSignupRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username) )
            throw new Exception("Username already taken");
        
        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.PhoneNumber))
            throw new Exception("Invalid phone number");
        
        Customer customer = new Customer(
            request.Username,
            request.Password,
            request.Name,
            request.PhoneNumber
            );
        dbManager.Customers.Add(customer);
        dbManager.SaveChanges();
        
        return new CustomerSignupResponse(
            authService.CreateToken(customer)
            );
    }

    public CustomerLoginResponse Login(CustomerLoginRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        Customer? customer = dbManager.Customers.FirstOrDefault(c => c.Username == request.Username && c.Password == request.Password);
        if (customer == null) 
            throw new UnauthorizedAccessException("Invalid username or password");
        
        return new CustomerLoginResponse(
            authService.CreateToken(customer)
            );
    }
}