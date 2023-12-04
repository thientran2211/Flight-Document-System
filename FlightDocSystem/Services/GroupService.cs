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

        public async Task<int> AddGroupAsync(GroupDTO groupDTO)
        {
            var group = new Group
            {
                GroupName = groupDTO.GroupName,
                PermissionID = groupDTO.PermissionID,
                CreateDate = DateTime.Now
            };

            context.Groups.Add(group);
            await context.SaveChangesAsync();

            return group.GroupID;
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

        public async Task UpdateGroupAsync(int id, GroupDTO group)
        {
            var existingGroup = await context.Groups.FindAsync(id);
            if (existingGroup != null) 
            {
                existingGroup.GroupName = group.GroupName;
                existingGroup.CreateDate= DateTime.Now;
                existingGroup.PermissionID= group.PermissionID;
                context.Groups.Update(existingGroup);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteGroupAsync(int id)
        {
            var existingGroup = context.Groups!.SingleOrDefault(g => g.GroupID == id);
            if (existingGroup != null)
            {
                context.Groups.Remove(existingGroup);
                await context.SaveChangesAsync();
            }
        }
    }
}
