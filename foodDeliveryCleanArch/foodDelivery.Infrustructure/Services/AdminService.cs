using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

namespace foodDelivery.Infrustructure.Services;

public class AdminService(DbManager dbManager, IAuthService authService) : IAdminService
{
    public AdminSignupResponse Signup(AdminSignupRequest request)
    {
        Token? token = authService.GetClaims();
        if (token == null)
            throw new UnauthorizedAccessException("Invalid token");
        if (token.Role != "Admin")
            throw new UnauthorizedAccessException("Invalid role");
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username) )
            throw new Exception("Username already taken");
        
        Admin admin = new Admin(
            request.Username,
            request.Password
            );
        dbManager.Admins.Add(admin);
        dbManager.SaveChanges();

        return new AdminSignupResponse(
            admin.Username,
            admin.Password
            );
    }
}