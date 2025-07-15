namespace foodDelivery.Domain;

public class Admin : User
{
    public Admin(string username, string password, string role) : base(username, password, role)
    {
    }
}