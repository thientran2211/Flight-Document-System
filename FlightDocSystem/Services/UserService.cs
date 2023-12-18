using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        
    }
}
