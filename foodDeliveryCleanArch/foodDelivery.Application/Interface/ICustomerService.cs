using foodDelivery.Application.DTOs.Customer;

namespace foodDelivery.Application.Interface;

public interface ICustomerService
{
    public CustomerSignupResponse Signup(CustomerSignupRequest request);
    public AutocompleteResponseDto AutocompleteRestaurants(string prefix);
}
