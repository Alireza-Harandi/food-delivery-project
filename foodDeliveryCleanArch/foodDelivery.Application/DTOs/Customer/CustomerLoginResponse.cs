namespace foodDelivery.Application.DTOs.Customer;

public class CustomerLoginResponse(string token)
{
    public string Token { get; set; } = token;
}