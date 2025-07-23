namespace foodDelivery.Domain;

public class Vendor : User
{
    public string OwnerName { get; set; }
    public string RestaurantName { get; set; }
    public string PhoneNumber { get; set; }
    public Location Location { get; set; }

    public Vendor(string username, string password, string ownerName, string restaurantName, string phoneNumber) : base(username, password, "Vendor")
    {
        OwnerName = ownerName;
        RestaurantName = restaurantName;
        PhoneNumber = phoneNumber;
    }

    public Vendor() {}
}