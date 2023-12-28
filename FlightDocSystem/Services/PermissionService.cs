using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly FlightDocsContext _context;

        public PermissionService(FlightDocsContext context) 
        {
            _context = context;
        }
        public async Task<Permission> AddPermissionAsync(PermissionDTO model)
        {
            if (model == null) throw new ArgumentNullException("Please enter permissionName");
            var permission = new Permission
            {
                PermissionName = model.PermissionName
            };
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task DeletePermissionAsync(int id)
        {
            var existingPermission = _context.Permissions.FirstOrDefault(p => p.PermissionID == id);
            if (existingPermission == null)
            {
                throw new KeyNotFoundException("PermissionId does not exist");
            }
            _context.Permissions.Remove(existingPermission);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Permission>> GetAllPermissionAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            return permissions;
        }

        public async Task<Permission> GetPermissionAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new KeyNotFoundException();
            }
            return permission;
        }

        public async Task<Permission> UpdatePermissionAsync(int id, PermissionDTO model)
        {
            var existingPermission = await _context.Permissions.FindAsync(id);
            if (existingPermission == null)
            {
                throw new KeyNotFoundException("Don't found permission of this id");
            }
            existingPermission.PermissionName = model.PermissionName;
            _context.Permissions.Update(existingPermission);
            await _context.SaveChangesAsync();
            return existingPermission;
        }
    }
}