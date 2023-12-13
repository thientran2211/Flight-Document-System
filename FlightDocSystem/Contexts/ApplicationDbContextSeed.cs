using FlightDocSystem.Constants;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace FlightDocSystem.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            // Seed default user
            var defaultUser = new User
            {
                UserName = Authorization.default_username,
                Email = Authorization.default_email
            };

            if (userManager.Users.All(u => u.UserID != defaultUser.UserID))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }

        internal static Task SeedEssentialsAsync(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            throw new NotImplementedException();
        }
    }
}
