using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface ITokenManager
    {
        string GenerateToken(User user, string role);
    }
}
