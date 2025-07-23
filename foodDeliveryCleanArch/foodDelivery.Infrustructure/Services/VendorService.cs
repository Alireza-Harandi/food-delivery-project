using System.Text.RegularExpressions;
using foodDelivery.Application.DTOs.Vendor;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

namespace foodDelivery.Infrustructure.Services;

public class VendorService(DbManager dbManager, IAuthService authService) : IVendorService
{
    public VendorSignupResponse Signup(VendorSignupRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username) )
            throw new Exception("Username already taken");
        
        var phoneRegex = new Regex(@"^(\+98|0)?9\d{9}$");
        if (!phoneRegex.IsMatch(request.PhoneNumber))
            throw new Exception("Invalid phone number");
        
        Vendor vendor = new Vendor(
            request.Username,
            request.Password,
            request.OwnerName,
            request.RestaurantName,
            request.PhoneNumber
        );
        dbManager.Vendors.Add(vendor);
        dbManager.SaveChanges();
        
        return new VendorSignupResponse(
            authService.CreateToken(vendor)
        );
    }
    
    public VendorSetLocationResponse SetLocation(VendorSetLocationRequest request)
    {
        Token? token = authService.GetClaims();
        if (token == null)
            throw new UnauthorizedAccessException("Invalid token");
        if (token.Role != "Vendor")
            throw new UnauthorizedAccessException("Invalid role");
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        Vendor? vendor = dbManager.Vendors.FirstOrDefault(v => v.Id == token.Id);
        if (vendor == null)
            throw new UnauthorizedAccessException("Invalid Vendor");
        
        vendor.Location = new Location(request.Latitude, request.Longitude, request.Address);;
        dbManager.SaveChanges();

        return new VendorSetLocationResponse(
            vendor.Id,
            vendor.Location.Latitude,
            vendor.Location.Longitude,
            vendor.Location.Address
        );
    }
}