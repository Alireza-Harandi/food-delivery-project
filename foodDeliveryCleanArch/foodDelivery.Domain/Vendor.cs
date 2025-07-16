namespace foodDelivery.Domain;

public class Vendor : User
{
    public string OwnerName { get; set; }
    public string RestaurantName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public Vendor(string username, string password, string ownerName, string restaurantName, string phoneNumber, string address) : base(username, password, "Vendor")
    {
        OwnerName = ownerName;
        RestaurantName = restaurantName;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public Vendor() {}
}