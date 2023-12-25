using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using FlightDocSystem.Requests;
using FlightDocSystem.Responses;

namespace FlightDocSystem.Interfaces
{
    public interface IUserService
    {
        Task<SignupResponse> RegisterUser(SignupRequest signupRequest);
        User CheckCredentials(LoginRequest user);
        string GetUserRole(int roleId);
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
        Task<LogoutResponse> LogoutAsync(int userId);
        public Task<List<User>> getAllUserAsync();
        public Task<User> getUserAsync(int id);
        public Task UpdateUserAsync(int id, UserDTO model);
        public Task DeleteUserAsync(int id);
    }
}
