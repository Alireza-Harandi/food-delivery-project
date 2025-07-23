namespace foodDelivery.Application.DTOs.Restaurant;

public class SetLocationRequest(double latitude, double longitude, string address, Guid restaurantId)
{
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
    public Guid RestaurantId { get; set; } = restaurantId;
}

public class SetLocationResponse(Guid restaurantId, double latitude, double longitude, string address)
{
    public Guid RestaurantId { get; set; } = restaurantId;
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
}