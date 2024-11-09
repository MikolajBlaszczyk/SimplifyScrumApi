using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;
using UserModule.Abstraction;

namespace UserModule.Informations;

public class RoleManagerHelper(UserManager<Teammate> userManager): IRoleManager
{
    public async Task<string> GetHighestSystemRoleAsync(Teammate teammate)
    {
        var roles =  await userManager.GetRolesAsync(teammate);

        string highestRole = SystemRole.User;

        foreach (var role in roles)
        {
            if (role == SystemRole.Admin)
                highestRole = role;
        }

        return highestRole;
    }
}