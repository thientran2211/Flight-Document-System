using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Requests;
using FlightDocSystem.Responses;
using FlightDocSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly FlightDocsContext _context;

        public UserController(IUserService userService, ITokenService tokenService, FlightDocsContext context) 
        {
            _userService = userService;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest(new TokenResponse
                    {
                        Error = $"{string.Join(",", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            var signupResponse = await _userService.SignupAsync(signupRequest);

            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }

            return Ok(signupResponse.Email);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var user = await _userService.getAllUserAsync();
                return Ok(user);
            }   
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.getUserAsync(id);
            return user != null ? Ok(user) : NotFound("User not found.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing login details",
                    ErrorCode = "L01"
                });
            }

            var loginResponse = await _userService.LoginAsync(loginRequest);

            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.Error
                });
            }

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO model)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userService.UpdateUserAsync(id, model);
            return Ok(existingUser);

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = _context.Users!.SingleOrDefault(b => b.UserID == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPost]
        [Route("Refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest == null || string.IsNullOrEmpty
            (refreshTokenRequest.RefreshToken) || refreshTokenRequest.UserID == 0)
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing refresh token details",
                    ErrorCode = "R01"
                });
            }

            var validateRefreshTokenResponse = await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);

            if (!validateRefreshTokenResponse.Success)
            {
                return UnprocessableEntity(validateRefreshTokenResponse);
            }

            var tokenResponse = await _tokenService.GenerateTokensAsync(validateRefreshTokenResponse.UserId);

            return Ok(new
            {
                AccessToken = tokenResponse.Item1,
                RefreshToken = tokenResponse.Item2,
            });
        }
    
        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var logout = await _userService.LogoutAsync(UserID);

            if (!logout.Success)
            {
                return UnprocessableEntity(logout);
            }

            return Ok("Logout succeeded!");
        }
        
    }
}
