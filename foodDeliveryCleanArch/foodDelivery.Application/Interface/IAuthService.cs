using foodDelivery.Domain;

namespace foodDelivery.Application.Interface;

public interface IAuthService
{
    public string CreateToken(User user);
    public Task<Token> CheckTokenAsync(Role role);
    public Task<Token> CheckTokenAsync();
    public Task<string> IsRevokedAsync();
}