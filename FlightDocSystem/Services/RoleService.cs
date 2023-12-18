using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly FlightDocsContext _context;

        public RoleService(FlightDocsContext context) 
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRoleAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                throw new NotImplementedException();
            }

            return role;
        }

        public async Task<int> AddRoleAsync(RoleDTO roleDTO)
        {
            var role = new Role
            {
                RoleName = roleDTO.RoleName
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role.RoleId;
        }

        public async Task UpdateRoleAsync(int id, RoleDTO roleDTO)
        {
            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole != null)
            {
                existingRole.RoleName = roleDTO.RoleName;
                _context.Roles.Update(existingRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            var existingRole = _context.Roles.SingleOrDefault(r => r.RoleId == id);
            if (existingRole != null)
            {
                _context.Roles.Remove(existingRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
