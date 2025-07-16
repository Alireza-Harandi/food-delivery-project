using foodDelivery.Application.DTOs.User;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;

namespace foodDelivery.Infrustructure.Services;

public class UserService(DbManager dbManager, IAuthService authService) : IUserService
{
    public UserLoginResponse Login(UserLoginRequest request)
    {
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        User? user = dbManager.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
        if (user == null) 
            throw new UnauthorizedAccessException("Invalid username or password.");
        
        return new UserLoginResponse(
            authService.CreateToken(user)
        );
    }
}