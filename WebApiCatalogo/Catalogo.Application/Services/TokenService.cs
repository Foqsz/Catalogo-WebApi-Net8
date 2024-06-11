using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiCatalogo.Catalogo.Application.Interface;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApiCatalogo.Catalogo.Application.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config)
        {
            // aqui eu obtenho o valor de SecretKey, que esta em SettingsJson
            var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid secret key!"); 

            //aqui eu converto pra um array de bits, passando a key(secretkey)
            var privateKey = Encoding.UTF8.GetBytes(key);

            //aqui são as credenciais de assinatura, usando a SecretKey.
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

            //aqui os valores estao vindo de settingsjson, para criar o token
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signingCredentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            
            randomNumberGenerator.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;

        }

        public ClaimsPrincipal GetPrincipalFromExperiredToken(string token, IConfiguration _config)
        {
            throw new NotImplementedException();
        }
    }
}
