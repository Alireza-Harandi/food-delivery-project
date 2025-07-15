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
        if (request.Username == null || request.Password == null)
            throw new Exception("Username or password is required");
        
        Admin admin = new Admin(
            request.Username,
            request.Password,
            "Admin"
            );
        dbManager.Users.Add(admin);
        dbManager.Admins.Add(admin);
        dbManager.SaveChanges();

        return new AdminSignupResponse(
            authService.CreateToken(admin)
            );
    }
}