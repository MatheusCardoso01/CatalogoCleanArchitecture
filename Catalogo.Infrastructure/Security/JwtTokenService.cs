using Catalogo.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateAccessToken(Usuario usuario)
    {
        var key = _config["Jwt:SecretKey"] ?? throw new InvalidOperationException("SecretKey not found in configuration.");
        var privateKey = Encoding.UTF8.GetBytes(key);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, usuario.Role ?? "User")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpireMinutes"])),
            Audience = _config["Jwt:Audience"],
            Issuer = _config["Jwt:Issuer"],
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var securityRandomBytes = new byte[128];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(securityRandomBytes);
        var refreshToken = Convert.ToBase64String(securityRandomBytes);

        return refreshToken;
    }

    public DateTime GetRefreshTokenExpiryTime()
    {
        var expireDays = int.Parse(_config["Jwt:RefreshTokenExpireDays"]);

        return DateTime.UtcNow.AddDays(expireDays);
    }
}
