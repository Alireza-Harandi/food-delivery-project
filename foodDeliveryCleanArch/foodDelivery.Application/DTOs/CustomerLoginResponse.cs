namespace foodDelivery.Application.DTOs;

public class CustomerLoginResponse(string token)
{
    public string Token { get; set; } = token;
}