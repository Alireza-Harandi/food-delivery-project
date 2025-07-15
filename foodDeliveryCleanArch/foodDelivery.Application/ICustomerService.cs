using foodDelivery.Application.DTOs;

namespace foodDelivery.Application;

public interface ICustomerService
{
    CustomerSignupResponse Signup(CustomerSignupRequest request);
}
