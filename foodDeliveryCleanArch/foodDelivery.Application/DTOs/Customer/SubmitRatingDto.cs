using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Customer;

public class SubmitRatingDto(Guid orderId, int score)
{
    public Guid OrderId { get; set; } = orderId;
    [Range(1, 5)] 
    public int Score { get; set; } = score;
}


