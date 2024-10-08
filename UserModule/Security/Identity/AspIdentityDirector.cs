using DataAccess.Model.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserModule.Exceptions;
using UserModule.Records;

namespace UserModule;

public class AspIdentityDirector
{
    private readonly SignInManager<Teammate> signInManager;
    private readonly UserManager<Teammate> userManager;

    public AspIdentityDirector(SignInManager<Teammate> signInManager, UserManager<Teammate> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<string> Login(AppUser user)
    {
        var result = await signInManager.PasswordSignInAsync(user.Username, user.Password.ToString(), true, false);

        var loggedUserName = signInManager.Context.User.Identity!.Name;
        var loggedUser = await userManager.FindByNameAsync(loggedUserName!);
        return loggedUser!.Id;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<string> CreateUser(Teammate teammate)
    {
        var result = await userManager.CreateAsync(teammate);
        if (result.Succeeded)
            await signInManager.SignInAsync(teammate, true);
        else
            throw new InternalIdentityException(result.Errors);

        var loggedUserName = signInManager.Context.User.Identity!.Name!;
        var loggedUser = await userManager.FindByNameAsync(loggedUserName);
        return loggedUser!.Id;
    }

    public async Task DeleteUser(Teammate teammate)
    {
        await userManager.DeleteAsync(teammate);
    }

    public async Task<Teammate> GetUserByGuid(string guid)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == guid);

        if (user == null)
            throw new Exception("user not found!");
        
        return user;
    }
   
}