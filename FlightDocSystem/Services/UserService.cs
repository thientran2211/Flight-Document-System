using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Repositories
{
    public class UserService : IUserService
    {
        private readonly FlightDocsContext _context;

        public UserService(FlightDocsContext context) 
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(UserDTO model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                IsActive = model.IsActive,
                GroupID = model.GroupID,
                RoleID = model.RoleID,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserID;
            
        }

        public async Task DeleteUserAsync(int id)
        {
            var existingUser = _context.Users!.SingleOrDefault(u => u.UserID == id);
            if (existingUser != null)
            {
                _context.Users!.Remove(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> getAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            
            return users;
        }

        public async Task<User> getUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new NotImplementedException();
            }    

            return user;
        }

        public async Task UpdateUserAsync(int id, UserDTO model)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser != null)
            {
                existingUser.UserName = model.UserName;
                existingUser.Phone = model.Phone;
                existingUser.Email = model.Email;
                existingUser.Password = model.Password;
                existingUser.IsActive = model.IsActive;

                if (model.GroupID > 0)
                {
                    existingUser.GroupID = model.GroupID;
                }
                if (model.RoleID > 0)
                {
                    existingUser.RoleID = model.RoleID;
                }
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
