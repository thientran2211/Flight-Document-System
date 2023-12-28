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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly FlightDocsContext _context;

        public PermissionController(IPermissionService permissionService, FlightDocsContext context)
        {
            _permissionService = permissionService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermission()
        {
            try
            {
                var permissions = await _permissionService.GetAllPermissionAsync();
                return Ok(permissions);
            }
            catch
            {
                return NotFound("Doesn't exist permissions");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            try
            {
                var permission = await _permissionService.GetPermissionAsync(id);
                return Ok(permission);
            }
            catch
            {
                return NotFound("Doesn't exist permission to this id!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(PermissionDTO permissionDTO)
        {
            try
            {
                var permission = await _permissionService.AddPermissionAsync(permissionDTO);
                return Ok(permission);
            }
            catch
            {
                return BadRequest("Please add appropriate information!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePermission(int id,  PermissionDTO model)
        {
            try
            {
                var permission = await _permissionService.UpdatePermissionAsync(id, model);
                return Ok(permission);
            }
            catch { return BadRequest("Update permission failed!"); }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePermission(int id)
        {
            try
            {
                await _permissionService.DeletePermissionAsync(id);
                return Ok("Delete permission succeeded!!!");
            }
            catch { return BadRequest("Delete permission failed!"); }
        }
    }
}
