using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record ReportRestaurantDto(
    [Required] Guid RestaurantId,
    [Required] string Description);