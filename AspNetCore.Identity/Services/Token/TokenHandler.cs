using AspNetCore.Identity.Abstractions.Token;
using AspNetCore.Identity.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AspNetCore.Identity.Services.Token;

public class TokenHandler : ITokenHandler
{
    readonly IConfiguration _configuration;
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public TokenDto CreateAccessToken(int minute)
    {
        TokenDto token = new();

        // Security Key 'in simetriğini alıyoruz.
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // Şifrelenmiş kimliği oluşturuyoruz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Oluşturulacak token ayarlarını veriyoruz.
        token.Expiration = DateTime.UtcNow.AddMinutes(minute);
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
            );

        // Token oluşturucu sınıfından bir örnek alalım.
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
