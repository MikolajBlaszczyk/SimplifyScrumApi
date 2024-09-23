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
    
    public async Task<SecurityResult> Login(SimpleUserModel userModel)
    {
        return await loginProcessor.LoginUser(userModel);
    }

    public async Task<SecurityResult> Logout()
    {
        return await logoutProcessor.LogoutCurrentUser();
    }

    public async Task<SecurityResult> SignIn(SimpleUserModel userModel)
    {
        return await userAccountProcessor.SignInUser(userModel);
    }

    public async Task<SecurityResult> Delete()
    {
        return await userAccountProcessor.DeleteCurrentUser();
    }
    public JwtSecurityToken GetToken(List<Claim> claims)
    {
        return tokenProvider.GetToken(claims);
    }

    public async Task<string> GetLoggedUsersGuid()
    {
        return await userAccountProcessor.GetCurrentUserId();
    }
}