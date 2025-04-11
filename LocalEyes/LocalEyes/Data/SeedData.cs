using Microsoft.AspNetCore.Identity;

namespace LocalEyes.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await CreateRolesAsync(roleManager);
            await CreateAdminUserAsync(userManager);
        }

        private static async Task CreateRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = { "Administrator", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    var role = new ApplicationRole { Name = roleName };

                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task CreateAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var userEmail = "danny_frandsen@hotmail.com";

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail
                };

                var result = await userManager.CreateAsync(newUser, "Velk0mmen!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Administrator");
                }
            }
        }
    }
}
