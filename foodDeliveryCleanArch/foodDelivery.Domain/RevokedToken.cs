using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Domain;

public class RevokedToken
{
    [Key]
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public RevokedToken(string token, DateTime expiryDate)
    {
        Id = Guid.NewGuid();
        Token = token;
        ExpiryDate = expiryDate;
    }

    public RevokedToken()
    {
    }
}
