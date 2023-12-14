using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly FlightDocsContext _context;

        public UserController(IUserService user, FlightDocsContext context) 
        {
            _user = user;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _user.getAllUserAsync();
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _user.getUserAsync(id);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = await _user.AddUserAsync(userDTO);
                    var user = await _user.getUserAsync(newUser);

                    return user != null ? Ok(user) : NotFound();
                }
                else
                {
                    return BadRequest("Email for user is invalid");
                }                              
            }
            catch
            {
                return BadRequest();
            } 

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            var existingUser = await _context.Users!.FindAsync(id);

            if (existingUser != null)
            {
                await _user.UpdateUserAsync(id, userDTO);
                return Ok(existingUser);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = _context.Users!.SingleOrDefault(u => u.UserID== id);
            if (existingUser != null)
            {
                await _user.DeleteUserAsync(id);
                return Ok("Delete Succeeded");
            }

            return BadRequest();
        }
    }
}
