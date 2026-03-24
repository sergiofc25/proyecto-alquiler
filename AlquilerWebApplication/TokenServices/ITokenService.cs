using Microsoft.IdentityModel.Tokens;
using Model.Entitie;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AlquilerWebApplication.TokenServices
{
    public interface ITokenService
    {
        SigningCredentials GetSigningCredentials();

        List<Claim> GetClaims(Ent_Usuario Usuario);

        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
