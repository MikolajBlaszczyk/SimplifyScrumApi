using DataAccess.Model.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserModule.Exceptions;
using UserModule.Records;

namespace UserModule;

public class AspIdentityDirector
{
    private readonly SignInManager<Teammate> signInManager;
    private readonly UserManager<Teammate> userManager;
    private readonly IHttpContextAccessor accessor;

    public AspIdentityDirector(SignInManager<Teammate> signInManager, UserManager<Teammate> userManager, IHttpContextAccessor accessor)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.accessor = accessor;
    }

    public async Task<bool> Login(AppUser user)
    { 
        var result = await signInManager.PasswordSignInAsync(user.Username, user.Password.ToString(), true, false);

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public async Task CreateUser(Teammate teammate, string password)
    {
        var createResult = await userManager.CreateAsync(teammate, password);
        if (createResult.Succeeded)
        {
            await signInManager.SignInAsync(teammate, true);
        }
            
        else
            throw new InternalIdentityException(createResult.Errors);
    }

    public async Task<bool> DeleteUserByGuid(string guid)
    {
        var teammate = await userManager.FindByIdAsync(guid);
        var deleteResult = await userManager.DeleteAsync(teammate!);
        return deleteResult.Succeeded;
    }

    public async Task<string> GetLoggedUserGUID()
    {
        var name = accessor.HttpContext.User.Identity?.Name;
        var user = await userManager.FindByNameAsync(name);
        
        return user!.Id;
    }
}