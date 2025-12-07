using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record SetOrderQuantityDto(
    [Required] Guid OrderId,
    [Required] Guid OrderItemId,
    [Required] int Quantity);