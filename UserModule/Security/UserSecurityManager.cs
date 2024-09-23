using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserModule.Abstraction;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Security;

public class UserSecurityManager : IManageSecurity
{
    private readonly LoginProcessor loginProcessor;
    private readonly LogoutProcessor logoutProcessor;
    private readonly UserAccountProcessor userAccountProcessor;
    private readonly TokenProvider tokenProvider;

    public UserSecurityManager(LoginProcessor loginProcessor, LogoutProcessor logoutProcessor, UserAccountProcessor userAccountProcessor, TokenProvider tokenProvider)
    {
        this.loginProcessor = loginProcessor;
        this.logoutProcessor = logoutProcessor;
        this.userAccountProcessor = userAccountProcessor;
        this.tokenProvider = tokenProvider;
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
    public JwtSecurityToken GetToken(List<Claim> claims)
    {
        return tokenProvider.GetToken(claims);
    }
}