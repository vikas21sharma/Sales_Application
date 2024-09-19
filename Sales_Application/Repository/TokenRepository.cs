using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sales_Application.Repository
{
    public class TokenRepository
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;

        public TokenRepository(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _key = configuration["Jwt:Key"];
        }

        //Shipper
        public string GenerateJwtToken(string password, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]// contain the information of admin,shipper and emp.
            {
            new Claim(JwtRegisteredClaimNames.Sub, password),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role) // Add role claim
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20), // Token expiration time
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
