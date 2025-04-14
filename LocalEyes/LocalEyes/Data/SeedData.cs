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
            await CreateAPIUserAsync(userManager);
            await CreateUserAsync(userManager);
        }

        private static async Task CreateRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = { "Administrator", "User", "API User" };

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

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager)
        {
            var userEmail = "user@dannyf.com";

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
                    await userManager.AddToRoleAsync(newUser, "User");
                }
            }
        }

        private static async Task CreateAPIUserAsync(UserManager<ApplicationUser> userManager)
        {
            var userEmail = "apiuser@dannyf.com";

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
                    await userManager.AddToRoleAsync(newUser, "API User");
                }
            }
        }
    }
}
