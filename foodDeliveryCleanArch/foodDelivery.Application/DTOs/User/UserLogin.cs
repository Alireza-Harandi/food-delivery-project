using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.User;

public record UserLoginRequest(
    [Required] string Username,
    [Required] string Password);

public record UserLoginResponse(string Token);