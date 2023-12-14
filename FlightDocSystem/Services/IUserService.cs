using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Repositories
{
    public interface IUserService
    {
        public Task<List<User>> getAllUserAsync();

        public Task<User> getUserAsync(int id);

        public Task<int> AddUserAsync(UserDTO model);

        public Task UpdateUserAsync(int id, UserDTO model);

        public Task DeleteUserAsync(int id);
    }
}
