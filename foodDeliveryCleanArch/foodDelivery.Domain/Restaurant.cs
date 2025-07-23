using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class Restaurant
{
    [Key]
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    [ForeignKey("VendorId")]
    public Vendor? Vendor { get; set; }
    
    public string Name { get; set; }
    public string Phone { get; set; }
    public Location? Location { get; set; }
    public List<WorkingHour> WorkingHours { get; set; } = new();
    public List<Menu> Menus { get; set; } = new();

    public Restaurant(Guid vendorId, string name, string phone)
    {
        Id = Guid.NewGuid();
        VendorId = vendorId;
        Name = name;
        Phone = phone;
    }

    public Restaurant() {}
}