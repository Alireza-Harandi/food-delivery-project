using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Customer
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public string PhoneNumber { get; set; }
    
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }

    public List<Order> Orders { get; set; } = new();

    public Customer(string name, string phoneNumber, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        PhoneNumber = phoneNumber;
        UserId = userId;
    }

    public Customer() { }
}