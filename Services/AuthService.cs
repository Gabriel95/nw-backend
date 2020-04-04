using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using nw_api.Data.Entities;
using nw_api.Interfaces;

namespace nw_api.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim("lastname", user.LastName),
                
            };
            
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(3600),
                signingCredentials: credentials
                );
            
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        public Guid GetUserIdFromToken(string token)
        {
            try
            {
                var securityTokenHandler = new JwtSecurityTokenHandler();
                if(!securityTokenHandler.CanReadToken(token)) return Guid.Empty;
                var decryptedToken = securityTokenHandler.ReadJwtToken(token);
                var claims = decryptedToken.Claims;
                return Guid.Parse(claims.ElementAt(2).Value);
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}