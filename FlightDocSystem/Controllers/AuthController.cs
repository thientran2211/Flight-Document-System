using FlightDocSystem.Authentication;
using FlightDocSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _service;

        public AuthController(IAuthentication service) 
        {
            _service = service;
        }

        [HttpPost("Login")]
        public IActionResult Login(Login model)
        {
            if (_service.ValidateUserCredentials(model.Email, model.Password))
            {
                var token = _service.GenerateJwtToken(model.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
