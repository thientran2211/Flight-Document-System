using FlightDocSystem.Services;
using Microsoft.AspNetCore.Mvc;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;
using FlightDocSystem.DTO;
using Microsoft.AspNetCore.Authorization;

namespace FlightDocSystem.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupList()
        {

            return Ok(await _groupService.GetGroupsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByID(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return BadRequest("Group not found.");
            }
            return Ok(group);
        }

        // API endpoint to Create Group
        [HttpPost]
        public async Task<IActionResult> Create(GroupDTO groupDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _groupService.AddGroupAsync(groupDTO);
                    return Ok("Thêm dữ liệu thành công!");
                }

                return BadRequest("Dữ liệu không hợp lệ.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi nội bộ: {ex.Message}");
            }
        }

        // API endpoint to Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupDTO groupDTO)
        {
            try
            {
                if (await _groupService.GroupExistsAsync(id))
                {
                    await _groupService.UpdateGroupAsync(id, groupDTO);
                    return Ok("Cập nhật dữ liệu thành công!");
                }
                else
                {
                    return NotFound("Không tìm thấy nhóm với Id đã cho");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi nội bộ: {ex.Message}");
            }
        }

        // API endpoint để Delete Group
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _groupService.GroupExistsAsync(id))
                {
                    await _groupService.DeleteGroupAsync(id);
                    return Ok("Xóa nhóm thành công!");
                }
                else
                {
                    return NotFound("Không tìm thấy nhóm với Id đã cho.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi nội bộ: {ex.Message}");
            }
        }
    }
}


