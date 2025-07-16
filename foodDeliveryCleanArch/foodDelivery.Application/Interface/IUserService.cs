using foodDelivery.Application.DTOs.User;

namespace foodDelivery.Application.Interface;

public interface IUserService
{
    UserLoginResponse Login(UserLoginRequest request);
}