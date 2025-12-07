using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Admin;

public record AdminSignupRequest(
    [Required] string Username,
    [Required] string Password);

public record AdminSignupResponse(string Username, string Password);