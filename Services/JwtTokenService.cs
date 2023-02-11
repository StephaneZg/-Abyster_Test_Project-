
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Abyster_Test_Project.Config;
using Abyster_Test_Project.Contract;
using Microsoft.IdentityModel.Tokens;

namespace Abyster_Test_Project.Services;

public class JwtTokenService : IJwtTokenService
{
    private JwtSettings _jwtSettings;
    public JwtTokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public JwtSecurityToken generateToken(List<Claim> userClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.secret));
        var expiryInMinutes = Convert.ToInt32(_jwtSettings.expiryInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.validIssuer,
            audience: _jwtSettings.validAudience,
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(expiryInMinutes),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public string generateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}