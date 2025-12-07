using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public record SubmitRatingDto(
    [Required] Guid OrderId,
    [Required][Range(1, 5)] int Score);