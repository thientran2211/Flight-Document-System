using FlightDocSystem.Services;
using Microsoft.AspNetCore.Mvc;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;
using FlightDocSystem.DTO;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupService _iGroupService;
        private readonly FlightDocsContext _context;

        public GroupController(IGroupService iGroup, FlightDocsContext context)
        {
            _iGroupService = iGroup;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            return Ok(await _iGroupService.GetGroupsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByID(int id)
        {
            var group = await _iGroupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return BadRequest("Group not found.");
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return Ok(await _context.Groups.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(int id, GroupDTO group)
        {
            var existingGroup = await _context.Groups.FindAsync(id);
            if (existingGroup== null)
            {
                return BadRequest("Group not found.");
            }
            await _iGroupService.UpdateGroupAsync(id, group);

            return Ok(await _context.Groups.ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = _context.Groups.SingleOrDefaultAsync(g => g.GroupID == id);
            if (group == null)
                return BadRequest("Group not found.");

            await _iGroupService.DeleteGroupAsync(id);

            return Ok(await _context.Groups.ToListAsync());
        }
    }
}
