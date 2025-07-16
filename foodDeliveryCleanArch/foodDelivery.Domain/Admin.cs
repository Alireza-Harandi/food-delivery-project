namespace foodDelivery.Domain;

public class Admin : User
{
    public Admin(string username, string password) : base(username, password, "Admin")
    {
    }
    
    public Admin() { }
}