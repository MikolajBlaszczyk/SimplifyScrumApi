using UserModule.Abstraction;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Security;

public class UserSecurityManager : IManageSecurity
{
    private readonly LoginProcessor loginProcessor;
    private readonly LogoutProcessor logoutProcessor;
    private readonly UserAccountProcessor userAccountProcessor;

    public UserSecurityManager(LoginProcessor loginProcessor, LogoutProcessor logoutProcessor, UserAccountProcessor userAccountProcessor)
    {
        this.loginProcessor = loginProcessor;
        this.logoutProcessor = logoutProcessor;
        this.userAccountProcessor = userAccountProcessor;
    }
    
    public async Task<SecurityResult> Login(AppUser user)
    {
        return await loginProcessor.LoginUser(user);
    }

    public async Task<SecurityResult> Logout()
    {
        return await logoutProcessor.LogoutCurrentUser();
    }

    public async Task<SecurityResult> SignIn(AppUser user)
    {
        return await userAccountProcessor.SignInUser(user);
    }

    public async Task<SecurityResult> Delete()
    {
        return await userAccountProcessor.DeleteCurrentUser();
    }
}