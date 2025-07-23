using foodDelivery.Application.DTOs.Admin;

namespace foodDelivery.Application.Interface;

public interface IAdminService
{
    public AdminSignupResponse Signup(AdminSignupRequest request);
}