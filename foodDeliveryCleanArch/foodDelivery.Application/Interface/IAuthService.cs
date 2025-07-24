using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IAuthService
{
    public string CreateToken(User user);
    public Token CheckToken(Role role);
    public Token CheckToken();
    public string IsRevoked();

}