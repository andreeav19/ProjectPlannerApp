using taskarescu.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace taskarescu.Server.Db;

public class SeedManager
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        await SeedRoles(serviceProvider);
        await SeedBaseUsers(serviceProvider);

    }

    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService < RoleManager<IdentityRole>>();

        await roleManager.CreateAsync(new IdentityRole(Role.Admin));
        await roleManager.CreateAsync(new IdentityRole(Role.Prof));
        await roleManager.CreateAsync(new IdentityRole(Role.Student));
    }

    private static async Task SeedBaseUsers(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "Admin");

        if (adminUser == null)
        {
            adminUser = new User { UserName = "Admin", Email = "admin@admin.com" };
            await userManager.CreateAsync(adminUser, "Admin1@");
            await userManager.AddToRoleAsync(adminUser, Role.Admin);
        }
    }
}
