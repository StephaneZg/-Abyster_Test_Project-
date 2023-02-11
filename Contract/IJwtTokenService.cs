
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Abyster_Test_Project.Contract;

public interface IJwtTokenService{
    JwtSecurityToken generateToken(List<Claim> userClaims);
    public string generateRefreshToken();
}