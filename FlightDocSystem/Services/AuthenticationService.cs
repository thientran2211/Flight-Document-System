using FlightDocSystem.Authentication;
using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocSystem.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly FlightDocsContext _context;

        public AuthenticationService(IConfiguration configuration, FlightDocsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJwtToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateUserCredentials(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }
    }
}
