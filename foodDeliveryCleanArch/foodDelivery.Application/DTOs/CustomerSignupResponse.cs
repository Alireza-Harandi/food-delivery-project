namespace foodDelivery.Application.DTOs;

public class CustomerSignupResponse(string token)
{
    public string Token { get; set; } = token;
}