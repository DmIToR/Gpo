using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InfoSystem.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace InfoSystem.Modules;

public class AuthManager : IAuthManager
{
    private readonly SymmetricSecurityKey _key;

    public AuthManager(string key)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    public string CreateToken(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.Now.AddHours(4),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}