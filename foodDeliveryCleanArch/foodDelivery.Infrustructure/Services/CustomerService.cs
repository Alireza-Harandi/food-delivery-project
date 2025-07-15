using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Customer;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

namespace foodDelivery.Infrustructure.Services;

public class CustomerService(DbManager dbManager, IAuthService authService) : ICustomerService
{
    public CustomerSignupResponse Signup(CustomerSignupRequest request)
    {
        if (request.Username == null || request.Password == null || request.Name == null)
            throw new ApplicationException("Username or password or name is required");
        if (dbManager.Users.Any(u => u.Username == request.Username) )
            throw new Exception("Username already taken");
        
        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.PhoneNumber))
            throw new Exception("Invalid phone number");
        
        Customer customer = new Customer(
            request.Username,
            request.Password,
            "Customer",
            request.Name,
            request.PhoneNumber
            );
        dbManager.Users.Add(customer);
        dbManager.Customers.Add(customer);
        dbManager.SaveChanges();
        
        return new CustomerSignupResponse(
            authService.CreateToken(customer)
            );
    }

    public CustomerLoginResponse Login(CustomerLoginRequest request)
    {
        if (request.Username == null || request.Password == null)
            throw new UnauthorizedAccessException("Username or password is required");
        User? user = dbManager.Customers.FirstOrDefault(c => c.Username == request.Username && c.Password == request.Password);
        if (user == null) 
            throw new Exception("Invalid username or password");
        
        return new CustomerLoginResponse(
            authService.CreateToken(user)
            );
    }
}