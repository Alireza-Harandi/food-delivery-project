namespace foodDelivery.Domain;

public class Token(Guid Id, string role)
{
    public Guid Id { get; set; } = Id;
    public string Role { get; set; } = role;
}