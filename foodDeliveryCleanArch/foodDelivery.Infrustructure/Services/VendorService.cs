using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class VendorService(DbManager dbManager, IAuthService authService) : IVendorService
{
    private Token CheckAccess()
    {
        Token token = authService.CheckToken(Role.Vendor);
        if (!dbManager.Vendors.Any(v => v.UserId == token.UserId))
            throw new UnauthorizedAccessException("Vendor not found");
        return token;
    }

    public VendorSignupResponse Signup(VendorSignupRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username))
            throw new AggregateException("Username already taken");

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.Phone))
            throw new ArgumentException("Invalid phone number");

        User user = new User(request.Username, request.Password, Role.Vendor);
        dbManager.Users.Add(user);
        Vendor vendor = new Vendor(user.Id, request.Name, request.Phone);
        dbManager.Vendors.Add(vendor);
        dbManager.SaveChanges();

        return new VendorSignupResponse(
            authService.CreateToken(user)
        );
    }

    public RegisterRestaurantResponse RegisterRestaurant(RegisterRestaurantRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        Token token = CheckAccess();

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.Phone))
            throw new ArgumentException("Invalid phone number");

        Restaurant restaurant = new Restaurant(
            dbManager.Vendors.First(v => v.UserId == token.UserId).Id,
            request.Name,
            request.Phone
        );

        dbManager.Restaurants.Add(restaurant);
        dbManager.SaveChanges();

        return new RegisterRestaurantResponse(
            restaurant.Id, restaurant.VendorId, restaurant.Name, restaurant.Phone
        );
    }

    public VendorProfileDto GetProfile()
    {
        Token token = CheckAccess();
        Vendor? vendor = dbManager.Vendors
            .Include(v => v.Restaurants)
            .FirstOrDefault(v => v.UserId == token.UserId);
        if (vendor == null)
            throw new UnauthorizedAccessException("Vendor not found");

        return new VendorProfileDto(
            vendor.Id,
            vendor.Name,
            vendor.Phone,
            vendor.Restaurants.Select(r => new RestaurantDetail(
                r.Id,
                r.Name
            )).ToList()
        );
    }
}