using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
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
        private readonly FlightDocsContext _context;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserService userService, FlightDocsContext context, ITokenManager tokenManager) 
        {
            _userService = userService;
            _context = context;
            _tokenManager = tokenManager;
        }

        [HttpPost("Register-User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(SignupRequest signupRequest)
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

            var result = await _userService.RegisterUser(signupRequest);

            return Ok();
        }

        [HttpPost("Check-Credentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthResponse> GetDetails(LoginRequest user)
        {
            var authUser = _userService.CheckCredentials(user);
            if (authUser == null)
            {
                return NotFound();
            }
            if (authUser != null && !BCrypt.Net.BCrypt.Verify(user.Password, authUser.Password))
            {
                return BadRequest("Incorrect Password! Please check your password!");
            }
            var roleName = _userService.GetUserRole(authUser.RoleId);
            var authResponse = new AuthResponse()
            {
                IsAuthenticated = true,
                Role = roleName,
                Token = _tokenManager.GenerateToken(authUser, roleName)
            };
            return Ok(authResponse);
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "System Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var user = await _userService.getAllUserAsync();
                return Ok(user);
            }   
            catch
            {
                return Unauthorized("Access denied!");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [Authorize(Roles = "System Admin")]
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

            return Ok(loginResponse);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "System Admin")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO model)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                await _userService.UpdateUserAsync(id, model);
                return Ok(existingUser);
            }
            catch
            {
                return Unauthorized("Access denied!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "System Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var existingUser = _context.Users!.SingleOrDefault(b => b.UserID == id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch { return Unauthorized("Access denied!"); }
            
        }

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
