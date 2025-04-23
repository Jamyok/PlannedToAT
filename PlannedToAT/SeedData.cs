using Microsoft.AspNetCore.Identity;
public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<PlannedToAT.Models.AdminUser>>(); 

        string adminRole = "Admin";

        // Ensure "Admin" role exists
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // Check if an admin user exists
        var adminEmail = "admin@example.com"; 
        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

        if (existingAdmin == null)
        {
            var adminUser = new PlannedToAT.Models.AdminUser()
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true,
                AdminRole = adminRole
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123"); 

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}
