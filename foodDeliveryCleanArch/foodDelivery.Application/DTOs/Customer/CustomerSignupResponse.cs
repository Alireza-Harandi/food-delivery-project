namespace foodDelivery.Application.DTOs.Customer;

public class CustomerSignupResponse(string token)
{
    public string Token { get; set; } = token;
}