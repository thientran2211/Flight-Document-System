using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoleAsync();
        public Task<Role> GetRoleByIdAsync(int id);
        public Task<int> AddRoleAsync(RoleDTO roleDTO);
        public Task UpdateRoleAsync(int id, RoleDTO roleDTO);
        public Task DeleteRoleAsync(int id);
    }
}
