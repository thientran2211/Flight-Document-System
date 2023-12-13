using FlightDocSystem.Services;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> ResultAsync(User user)
        {
            var result = await _userService.RegisterAsyn(user);
            return Ok(result);
        }
    }
}
