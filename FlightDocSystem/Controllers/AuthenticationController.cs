using FlightDocSystem.Data;
using FlightDocSystem.Models;
using FlightDocSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly FlightDocsContext _context;

        public AuthenticationController(IAuthenticationService service, FlightDocsContext context) 
        {
            _service = service;
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var user = _service.AuthenticateUser(userLogin);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var token = _service.GenerateToken(user);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            return Ok(token);
        }
    }
}
