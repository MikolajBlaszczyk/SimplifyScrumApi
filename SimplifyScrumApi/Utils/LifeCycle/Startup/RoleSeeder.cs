using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserModule.Informations;

namespace SimplifyScrum.Utils.LifeCycle.Startup;

public static class RoleSeeder
{
  
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { SystemRole.TeamAdmin, SystemRole.User };

            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        
    }

    public static async Task AddAdminRolesForSelectedUsers(IServiceProvider serviceProvider, string[] guids)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Teammate>>();

            foreach (var guid in guids)
            {
                var user = userManager.Users.FirstOrDefault(u => u.Id == guid);
                if (user is not null)
                {
                    if(await userManager.IsInRoleAsync(user, SystemRole.TeamAdmin))
                        await userManager.AddToRoleAsync(user, SystemRole.TeamAdmin);
                }
            }
        }
    }
}