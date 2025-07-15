using foodDelivery.Application.DTOs.Admin;

namespace foodDelivery.Application.Interface;

public interface IAdminService
{
    AdminSignupResponse Signup(AdminSignupRequest request);
    AdminLoginResponse Login(AdminLoginRequest request);
}