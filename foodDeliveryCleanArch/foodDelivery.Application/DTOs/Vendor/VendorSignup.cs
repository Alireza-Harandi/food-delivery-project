using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Vendor;

public record VendorSignupRequest(
    [Required] string Username,
    [Required] string Password,
    [Required] string Name,
    [Required] string Phone);

public record VendorSignupResponse(string Token);