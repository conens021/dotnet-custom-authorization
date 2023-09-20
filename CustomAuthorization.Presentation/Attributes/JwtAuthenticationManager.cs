using CustomAuthorization.BLL.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomAuthorization.Presentation.Attributes
{
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccesor;

        public JwtAuthenticationManager(IConfiguration configuration, IHttpContextAccessor contextAccesor)
        {
            _configuration = configuration;
            _contextAccesor = contextAccesor;
        }
        
        public string Authenticate(AuthUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_configuration["AppSettings:EncryptionKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
