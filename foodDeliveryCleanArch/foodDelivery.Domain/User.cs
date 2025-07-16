using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Domain;

public abstract class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    protected User(string username, string password, string role)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Role = role;
    }

    protected User() { }
}