using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTService.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace JWTService.BLL.Services;

public static class JwtService
{
    internal static string GenerateAccessToken(JwtOptions options, ClaimsIdentity identity)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
        var signingCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = identity,
            Issuer = options.ValidIssuer,
            Audience = options.ValidAudience,
            Expires = DateTime.Now.AddMinutes(options.TokenLifeTime),
            SigningCredentials = signingCreds
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
    


    internal static string GenerateRefreshToken(JwtOptions options)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, "refresh_token")
        };

        var expiryDate = DateTime.UtcNow.AddDays(10);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiryDate,
            SigningCredentials = signingCredentials
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }
}