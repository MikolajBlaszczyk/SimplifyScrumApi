using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace UserModule;

public class TokenProvider(IConfiguration configuration)
{
    internal JwtSecurityToken GetToken(List<Claim> claims)
    {
        var pwd = configuration["JWT:Secret"]!;
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pwd));

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}