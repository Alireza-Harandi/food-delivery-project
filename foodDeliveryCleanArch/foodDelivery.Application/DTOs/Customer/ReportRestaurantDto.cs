namespace foodDelivery.Application.DTOs.Customer;

public class ReportRestaurantDto(Guid restaurantId, string description)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public string Description { get; set; } = description;
}