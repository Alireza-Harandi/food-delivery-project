using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Vendor
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    
    public string Name { get; set; }
    public string Phone { get; set; }
    public List<Restaurant> Restaurants { get; set; } = new();

    public Vendor(Guid userId, string name, string phone)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Phone = phone;
    }

    public Vendor() {}
}