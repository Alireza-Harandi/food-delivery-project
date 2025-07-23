namespace foodDelivery.Application.DTOs.Customer;

public class FinalizeOrderRequest(Guid orderId, double latitude, double longitude, string address)
{
    public Guid OrderId { get; set; } = orderId;
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
    public string Address { get; set; } = address;
}

public class FinalizeOrderResponse(Guid orderId)
{
    public Guid OrderId { get; set; } = orderId;
}