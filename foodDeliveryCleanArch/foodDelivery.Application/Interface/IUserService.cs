using foodDelivery.Application.DTOs.User;
using foodDelivery.Application.DTOs.Vendor;

namespace foodDelivery.Application.Interface;

public interface IUserService
{
    UserLoginResponse Login(UserLoginRequest request);
}