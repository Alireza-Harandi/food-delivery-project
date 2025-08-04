using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record FinalizeOrderRequest(
    [Required] Guid OrderId,
    [Required] double Latitude,
    [Required] double Longitude,
    [Required] string Address);

public record FinalizeOrderResponse(Guid OrderId);