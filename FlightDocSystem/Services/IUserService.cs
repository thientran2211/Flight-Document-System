using FlightDocSystem.Models;

namespace FlightDocSystem.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsyn(User model);
    }
}
