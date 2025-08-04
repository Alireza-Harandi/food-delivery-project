using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record CustomerSignupRequest(
    [Required] string Username,
    [Required] string Password,
    [Required] string Name,
    [Required] string PhoneNumber);

public record CustomerSignupResponse(string Token);