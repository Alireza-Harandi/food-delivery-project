using foodDelivery.Application.DTOs.Customer;

namespace foodDelivery.Application.Interface;

public interface ICustomerService
{
    CustomerSignupResponse Signup(CustomerSignupRequest request);
    CustomerLoginResponse Login(CustomerLoginRequest request);
}
