using foodDelivery.Application.DTOs.User;

namespace foodDelivery.Application.Interface;

public interface IUserService
{
    public UserLoginResponse Login(UserLoginRequest request);
    public void Logout();
}