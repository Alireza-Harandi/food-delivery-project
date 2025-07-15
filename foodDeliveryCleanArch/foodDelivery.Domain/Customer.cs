namespace foodDelivery.Domain;

public class Customer : User
{
    public string Name { get; set; }    
    public string PhoneNumber { get; set; }

    public Customer(string username, string password, string role, string name, string phoneNumber) : base(username, password, role)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}