using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public User(string username, string password, Role role)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Role = role;
    }

    public User() { }
}