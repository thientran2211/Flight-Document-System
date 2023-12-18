using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Requests;
using FlightDocSystem.Responses;
using FlightDocSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UserController(IUserService userService, ITokenService tokenService) 
        {
            this.userService = userService;
            this.tokenService = tokenService;
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

            var signupResponse = await userService.SignupAsync(signupRequest);

            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }

            return Ok(signupResponse.Email);
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

            var loginResponse = await userService.LoginAsync(loginRequest);

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

            var validateRefreshTokenResponse = await tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);

            if (!validateRefreshTokenResponse.Success)
            {
                return UnprocessableEntity(validateRefreshTokenResponse);
            }

            var tokenResponse = await tokenService.GenerateTokensAsync(validateRefreshTokenResponse.UserId);

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
            var logout = await userService.LogoutAsync(UserID);

            if (!logout.Success)
            {
                return UnprocessableEntity(logout);
            }

            return Ok("Logout succeeded!");
        }
        
    }
}
