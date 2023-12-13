using Microsoft.AspNetCore.Identity;

namespace FlightDocSystem.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            User
        }

        public const string default_username = "user";
        public const string default_email = "user@vietjetair.com";
        public const string default_password = "Pa$$word.";
        public const Roles default_role = Roles.User;

        internal static void SeedData(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            throw new NotImplementedException();
        }
    }
}
