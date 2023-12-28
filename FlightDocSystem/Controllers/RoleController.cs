using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "System Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly FlightDocsContext _context;

        public RoleController(IRoleService roleService, FlightDocsContext context) 
        {
            _roleService = roleService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoleAsync();
                return Ok(roles);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return role != null ? Ok(role) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRole(RoleDTO roleDTO)
        {
            try
            {
                var newRole = await _roleService.AddRoleAsync(roleDTO);
                var role = await _roleService.GetRoleByIdAsync(newRole);
                return role != null ? Ok(role) : NotFound();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO roleDTO)
        {
            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole != null)
            {
                await _roleService.UpdateRoleAsync(id, roleDTO);
                return Ok(existingRole);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var existingRole = _context.Roles.SingleOrDefault(r => r.RoleId == id);
            if (existingRole != null)
            {
                await _roleService.DeleteRoleAsync(id);
                return Ok();
            }

            return BadRequest();
        }
    }
}
