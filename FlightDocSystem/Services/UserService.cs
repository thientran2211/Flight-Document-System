using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Responses;
using FlightDocSystem.Requests;
using FlightDocSystem.Helper;

namespace FlightDocSystem.Services
{
    public class UserService : IUserService
    {
        private readonly FlightDocsContext _context;
        private readonly ITokenService _tokenService;

        public UserService(FlightDocsContext context, ITokenService tokenService) 
        {
            _context = context;
            _tokenService = tokenService;
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

        public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = _context.Users.SingleOrDefault(user => user.IsActive && user.Email == loginRequest.Email);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Email not found",
                    ErrorCode = "L02"
                };
            }

            var passwordHash = PasswordHelper.HashUsingPBKDF2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

            if (user.Password != passwordHash)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Invalid Password",
                    ErrorCode = "L03"
                };
            }

            var token = await System.Threading.Tasks.Task.Run(() =>
                        _tokenService.GenerateTokensAsync(user.UserID));

            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2
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

        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
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

            var salt = PasswordHelper.GetSecureSalt();
            var passwordHash = PasswordHelper.HashUsingPBKDF2(signupRequest.Password, salt);

            var user = new User
            {
                Email = signupRequest.Email,
                Password = passwordHash,
                PasswordSalt = Convert.ToBase64String(salt),
                UserName = signupRequest.UserName,
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
