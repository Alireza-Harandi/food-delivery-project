using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrastructure.Services;

public class VendorService(DbManager dbManager, IAuthService authService) : IVendorService
{
    private async Task<Token> CheckAccessAsync()
    {
        Token token = await authService.CheckTokenAsync();
        bool exists = await dbManager.Vendors.AnyAsync(v => v.UserId == token.UserId);
        if (!exists)
            throw new UnauthorizedAccessException("Vendor not found");
        return token;
    }

    public async Task<VendorSignupResponse> SignupAsync(VendorSignupRequest request)
    {
        bool usernameTaken = await dbManager.Users.AnyAsync(u => u.Username == request.Username);
        if (usernameTaken)
            throw new AggregateException("Username already taken");

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.Phone))
            throw new ArgumentException("Invalid phone number");

        User user = new User(request.Username, request.Password, Role.Vendor);
        await dbManager.Users.AddAsync(user);
        Vendor vendor = new Vendor(user.Id, request.Name, request.Phone);
        await dbManager.Vendors.AddAsync(vendor);
        await dbManager.SaveChangesAsync();

        return new VendorSignupResponse(
            authService.CreateToken(user)
        );
    }

    public async Task<RegisterRestaurantResponse> RegisterRestaurantAsync(RegisterRestaurantRequest request)
    {
        Token token = await CheckAccessAsync();

        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.Phone))
            throw new ArgumentException("Invalid phone number");

        var vendor = await dbManager.Vendors.FirstAsync(v => v.UserId == token.UserId);

        Restaurant restaurant = new Restaurant(
            vendor.Id,
            request.Name,
            request.Phone
        );

        await dbManager.Restaurants.AddAsync(restaurant);
        await dbManager.SaveChangesAsync();

        return new RegisterRestaurantResponse(
            restaurant.Id, restaurant.VendorId, restaurant.Name, restaurant.Phone
        );
    }

    public async Task<VendorProfileDto> GetProfileAsync()
    {
        Token token = await CheckAccessAsync();

        Vendor? vendor = await dbManager.Vendors
            .Include(v => v.Restaurants)
            .FirstOrDefaultAsync(v => v.UserId == token.UserId);

        if (vendor == null)
            throw new UnauthorizedAccessException("Vendor not found");
        Console.WriteLine($"{vendor.Id}, {vendor.Name}, {vendor.Phone}");

        VendorProfileDto result = new VendorProfileDto(
            vendor.Id,
            vendor.Name,
            vendor.Phone,
            vendor.Restaurants.Select(r => new RestaurantDetail(
                r.Id,
                r.Name
            )).ToList()
        );

        return result;
    }
}