using foodDelivery.Application.DTOs.User;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class UserService(DbManager dbManager, IAuthService authService) : IUserService
{
    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");

        User? user = await dbManager.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password.");

        return new UserLoginResponse(
            authService.CreateToken(user)
        );
    }

    public async Task LogoutAsync()
    {
        string token = await authService.IsRevokedAsync();

        await dbManager.RevokedTokens.AddAsync(new RevokedToken(
            token,
            DateTime.UtcNow.AddMinutes(10)
        ));

        await dbManager.SaveChangesAsync();
    }
}