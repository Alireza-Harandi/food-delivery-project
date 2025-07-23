namespace foodDelivery.Domain;

public class Token(Guid userId, Role role)
{
    public Guid UserId { get; set; } = userId;
    public Role Role { get; set; } = role;
}