namespace foodDelivery.Application.DTOs.Customer;

public class CustomerProfileDto(Guid customerId, string name, string phoneNumber)
{
    public Guid CustomerId { get; set; } = customerId;
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = phoneNumber;
}