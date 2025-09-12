using Domain.Sequrity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Api.Sequrity.Services
{
    public class JwtSecurityService(IConfiguration configuration) : IJwtSecurityService
    {
        public string CreateToken(CustomIdentityUser user)
        {
            string secretKey = configuration["AuthSettings:SecretKey"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("is_premium", "true")
            };
              
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenHandler = new JsonWebTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow.AddMinutes(0), 
                Expires = DateTime.UtcNow.AddMinutes(10)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
