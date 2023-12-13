using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using FlightDocSystem.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace FlightDocSystem.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt) 
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _jwt = jwt.Value;
        }

        public async Task<string> RegisterAsyn(User model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} is already registered.";
            }
        }
    }
}
