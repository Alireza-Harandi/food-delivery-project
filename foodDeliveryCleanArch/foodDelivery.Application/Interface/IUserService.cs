using foodDelivery.Application.DTOs.User;

namespace foodDelivery.Application.Interface;

public interface IUserService
{
    public Task<UserLoginResponse> LoginAsync(UserLoginRequest request);
    public Task LogoutAsync();
}