using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApiCatalogo.Catalogo.Application.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration);

        string GenerateAcessToken();

        ClaimsPrincipal GetPrincipalFromExperiredToken(string token, IConfiguration _config);
    }
}
