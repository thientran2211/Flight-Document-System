using FlightDocSystem.Models;
using System.Security.Claims;

namespace FlightDocSystem.Services
{
    public interface IAuthenticationService
    {
        public User AuthenticateUser(UserLogin userLogin);
        public Token GenerateToken(User user);        
    }
}
