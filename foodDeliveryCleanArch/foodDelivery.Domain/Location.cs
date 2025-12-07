using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Location
{
    [Key] public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    [ForeignKey("RestaurantId")] public Restaurant? Restaurant { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; }

    public Location(double latitude, double longitude, string address, Guid restaurantId)
    {
        Id = Guid.NewGuid();
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
        RestaurantId = restaurantId;
    }
}