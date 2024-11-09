using Microsoft.AspNetCore.Identity;
using UserModule.Informations;

namespace SimplifyScrum.Utils.LifeCycle.Startup;

public static class RoleSeeder
{
  
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { SystemRole.Admin, SystemRole.User };

        foreach (var role in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}