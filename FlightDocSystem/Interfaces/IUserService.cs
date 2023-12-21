using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using FlightDocSystem.Requests;
using FlightDocSystem.Responses;

namespace FlightDocSystem.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        Task<LogoutResponse> LogoutAsync(int userId);
        public Task<List<User>> getAllUserAsync();
        public Task<User> getUserAsync(int id);
        public Task UpdateUserAsync(int id, UserDTO model);
        public Task DeleteUserAsync(int id);
    }
}
