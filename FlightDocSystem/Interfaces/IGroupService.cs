using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IGroupService
    {
        public Task<List<Group>> GetGroupsAsync();
        public Task<Group> GetGroupByIdAsync(int id);
        public Task AddGroupAsync(GroupDTO groupDTO);
        public Task UpdateGroupAsync(int groupID, GroupDTO groupDTO);
        public Task DeleteGroupAsync(int groupID);
        Task<bool> GroupExistsAsync(int groupId);
    }
}
