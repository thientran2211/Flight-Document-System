using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IPermissionService
    {
        public Task<List<Permission>> GetAllPermissionAsync();
        public Task<Permission> GetPermissionAsync(int id);
        public Task<Permission> AddPermissionAsync(PermissionDTO permission);
        public Task<Permission> UpdatePermissionAsync(int id, PermissionDTO permission);
        public Task DeletePermissionAsync(int id);
    }
}
