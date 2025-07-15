using foodDelivery.Domain;
namespace foodDelivery.Application;

public interface IAuthService
{
    string CreateToken(User user);
}