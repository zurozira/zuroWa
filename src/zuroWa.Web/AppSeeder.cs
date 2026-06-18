using Microsoft.AspNetCore.Identity;
using zuroWa.Core.Domain;

namespace zuroWa.Web;

// This class will be in .gitignore
public class AppSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        // Check if there is Admin role, if not create the role
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create admin user
        var existingAdminUser = await userManager.FindByNameAsync("zuro");
        if (existingAdminUser == null)
        {
            AppUser adminUser = new AppUser { UserName = "zuro" };

            var result = await userManager.CreateAsync(adminUser, "Congnguyen175@");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}

