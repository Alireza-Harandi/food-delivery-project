using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace foodDelivery.Infrastructure.Services;

public class AuthService(IConfiguration configuration, IHttpContextAccessor httpContext, DbManager dbManager)
    : IAuthService
{
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var key = configuration["Jwt:Key"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Token? GetClaims()
    {
        var userClaims = httpContext.HttpContext?.User;
        if (userClaims == null || !userClaims.Identity?.IsAuthenticated == true)
            return null;

        var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
        var roleClaim = userClaims.FindFirst(ClaimTypes.Role);

        if (userIdClaim == null || roleClaim == null)
            return null;

        if (!Enum.TryParse<Role>(roleClaim.Value, out var roleEnum))
            return null;

        return new Token(Guid.Parse(userIdClaim.Value), roleEnum);
    }

    public async Task<Token> CheckTokenAsync()
    {
        Token? token = GetClaims();
        if (token == null)
            throw new UnauthorizedAccessException("Invalid token");

        await IsRevokedAsync();
        return token;
    }

    public async Task<string> IsRevokedAsync()
    {
        var tokenHeader = httpContext.HttpContext?.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(tokenHeader) || !tokenHeader.StartsWith("Bearer "))
            throw new UnauthorizedAccessException("Token is missing.");

        var token = tokenHeader.Replace("Bearer ", "");

        bool isRevoked = await dbManager.RevokedTokens.AnyAsync(rt => rt.Token == token);

        if (isRevoked)
            throw new UnauthorizedAccessException("This token has been revoked.");

        return token;
    }
}