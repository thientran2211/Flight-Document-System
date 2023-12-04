using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Services
{
    public interface IGroupService
    {
        public Task<List<Group>> GetGroupsAsync();
        public Task<Group> GetGroupByIdAsync(int id);
        public Task<int> AddGroupAsync(GroupDTO group);
        public Task UpdateGroupAsync(int id, GroupDTO group);
        public Task DeleteGroupAsync(int id);
    }
}
