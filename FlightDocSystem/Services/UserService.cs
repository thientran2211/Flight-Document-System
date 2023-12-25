using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Responses;
using FlightDocSystem.Requests;

namespace FlightDocSystem.Services
{
    public class UserService : IUserService
    {
        private readonly FlightDocsContext _context;
        private readonly ITokenManager _tokenManager;

        public UserService(FlightDocsContext context, ITokenManager tokenManager) 
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public User CheckCredentials(LoginRequest user)
        {
            var userCredentials = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            return userCredentials;
        }

        public async Task<SignupResponse> RegisterUser(SignupRequest signupRequest)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(user => user.Email == signupRequest.Email);

            if (existingUser != null)
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "User already exists with the same email",
                    ErrorCode = "S02"
                };
            }

            if (signupRequest.Password != signupRequest.ConfirmPassword)
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "Password and confirm password do not match",
                    ErrorCode = "S03"
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);
            signupRequest.Password = passwordHash;

            var user = new User
            {
                Email = signupRequest.Email,
                UserName = signupRequest.UserName,
                Password = passwordHash,
                GroupId = signupRequest.GroupId,
                RoleId = signupRequest.RoleId,
                IsActive = true
            };

            await _context.Users.AddAsync(user);

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SignupResponse { Success = true, Email = user.Email };
            }

            return new SignupResponse
            {
                Success = false,
                Error = "Unable to save the user",
                ErrorCode = "S05"
            };
        }

        public string GetUserRole(int roleId)
        {
            var roleName = _context.Roles.SingleOrDefault(u => u.RoleId == roleId).RoleName;
            return roleName;
        }

        public async Task<List<User>> getAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> getUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            return user;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.IsActive && user.Email == user.Email);

            var token = _tokenManager.GenerateToken(user, GetUserRole(user.RoleId));
            var roleName = GetUserRole(user.RoleId);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Token = token,
                Role = roleName
            };
        }

        public async Task<LogoutResponse> LogoutAsync(int userId)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == userId);

            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }

            var saveResponse = await _context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }

            return new LogoutResponse
            {
                Success = false,
                Error = "Unable to logout user",
                ErrorCode = "L04"
            };
        }

        public async Task UpdateUserAsync(int id, UserDTO model)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.UserName = model.UserName;
                existingUser.Phone = model.Phone;
                existingUser.Email = model.Email;
                existingUser.Password = model.Password;
                existingUser.IsActive = model.IsActive;
                
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var existingUser = _context.Users!.SingleOrDefault(b => b.UserID == id);
            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
            }
        }       
    }
}
