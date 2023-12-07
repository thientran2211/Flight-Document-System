using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class GroupService : IGroupService
    {
        private readonly FlightDocsContext context;

        public GroupService(FlightDocsContext context)
        {
            this.context = context;
        }

        public async Task AddGroupAsync(GroupDTO groupDTO)
        {
            var group = new Group
            {
                GroupName = groupDTO.GroupName,
                PermissionID = groupDTO.PermissionID,
                NumberOfUser = groupDTO.NumberOfUser,
                CreateDate = DateTime.Now
            };

            context.Groups.Add(group);

            await context.SaveChangesAsync();
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            var groups = await context.Groups.ToListAsync();
            return groups;
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {

            var groups = await context.Groups.FindAsync(id);
            if (groups == null)
            {
                throw new NotImplementedException();
            }
            return groups;
        }

        public async Task UpdateGroupAsync(int groupId, GroupDTO groupDTO)
        {
            var existingGroup = await context.Groups.FirstOrDefaultAsync(g => g.GroupID == groupId);
            if (existingGroup != null)
            {
                existingGroup.GroupName = groupDTO.GroupName;
                existingGroup.PermissionID = groupDTO.PermissionID;
                existingGroup.NumberOfUser = groupDTO.NumberOfUser;
                existingGroup.CreateDate = DateTime.Now;
                context.Groups.Update(existingGroup);

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            if (await GroupExistsAsync(groupId))
            {
                var groupToDelete = await context.Groups.FindAsync(groupId);

                if (groupToDelete != null)
                {
                    context.Groups.Remove(groupToDelete);
                    await context.SaveChangesAsync();
                }

            }
        }

        public async Task<bool> GroupExistsAsync(int groupId)
        {
            return await context.Groups.AnyAsync(g => g.GroupID == groupId);
        }
    }
}
