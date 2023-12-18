using FlightDocSystem.Data;
using FlightDocSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FlightDocSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly FlightDocsContext _context;

        public AuthenticationService(IConfiguration configuration, FlightDocsContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public Token GenerateToken(User user)
        {
            var currentUser = GetUser(user.Email, user.Password);

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();

                bool isAdmin = currentUser.RoleID == 1;

                if (isAdmin)
                {
                    var adminClaim = new Claim(ClaimTypes.Role, "Admin");                    
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var accessToken = tokenHandler.CreateJwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    subject: new ClaimsIdentity(claims),
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials);

                return new Token
                {
                    AccessToken = tokenHandler.WriteToken(accessToken)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User AuthenticateUser(UserLogin userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Email.ToLower() == userLogin.Email.ToLower() && u.Password == userLogin.Password);

            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
        }
        
        public User GetUser (string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
