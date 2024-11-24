using DataAccess.Model.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserModule.Exceptions;
using UserModule.Informations;
using UserModule.Records;

namespace UserModule;

public class AspIdentityDirector(
        SignInManager<Teammate> signInManager,
        UserManager<Teammate> userManager,
        RoleManager<IdentityRole> roleManager,
        IHttpContextAccessor accessor)
{

    public async Task<bool> LoginAsync(SimpleUserModel userModel)
    { 
        var result = await signInManager.PasswordSignInAsync(userModel.Username, userModel.Password, true, false);

        return result.Succeeded;
    }
    
    public async Task<bool> LoginAsync(Teammate userModel, string password)
    { 
        var result = await signInManager.PasswordSignInAsync(userModel.UserName, password, true, false);

        return result.Succeeded;
    }
    
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task CreateUserAsync(Teammate teammate, string password)
    {
        if (userManager.Users.Any(u => u.UserName == teammate.UserName))
            throw new Exception("User already exsists");
            
        var createResult = await userManager.CreateAsync(teammate, password);

        if (createResult.Succeeded)
        {
            await LoginAsync(teammate, password);
        }
        else
        {
            throw new InternalIdentityException(createResult.Errors);
        }
    }

    public async Task<Teammate?> GetUserByGUIDAsync(string guid)
    {
        return await userManager.FindByIdAsync(guid);
    }
    
    public async Task DeleteUserByGUIDAsync(string guid)
    {
        var teammate = await GetUserByGUIDAsync(guid);
        await userManager.DeleteAsync(teammate!);
    }

    public async Task<string> GetLoggedUserGUIDAsync()
    {
        var name = accessor.HttpContext.User.Identity?.Name;
        var user = await userManager.FindByNameAsync(name);
        
        return user!.Id;
    }

    public async Task<Teammate> GetCurrentUserAsync()
    {
        var name = accessor.HttpContext.User.Identity?.Name;
        var user = await userManager.FindByNameAsync(name);

        if (user is null)
            throw new Exception();
        
        return user;
    }
    
    public async Task AddRoleForUserAsync(Teammate user, string role)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            throw new Exception();
        }
        
        await userManager.AddToRoleAsync(user, role);
    }

    public async Task<List<string>> GetUserRolesAsync(Teammate user)
    {
        return await userManager.GetRolesAsync(user) as List<string>;
    }
}