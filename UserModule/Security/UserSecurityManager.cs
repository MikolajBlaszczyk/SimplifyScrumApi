using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccess.Model.User;
using UserModule.Abstraction;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Security;

public class UserSecurityManager(
    LoginProcessor loginProcessor,
    LogoutProcessor logoutProcessor,
    UserAccountProcessor userAccountProcessor,
    TokenProvider tokenProvider
    ) : IManageSecurity
{
    public async Task<SecurityResult> LoginAsync(SimpleUserModel userModel)
    {
        return await loginProcessor.LoginUserAsync(userModel);
    }

    public async Task<SecurityResult> LogoutAsync()
    {
        return await logoutProcessor.LogoutCurrentUserAsync();
    }

    public async Task<SecurityResult> SignInAsync(SimpleUserModel userModel)
    {
        return await userAccountProcessor.SignInUserAsync(userModel);
    }

    public async Task<SecurityResult> DeleteAsync()
    {
        return await userAccountProcessor.DeleteCurrentUserAsync();
    }

    public async Task<SecurityResult> AddRoleAsyncForCurrentUser(string role)
    {
        return await userAccountProcessor.AddRoleToCurrentUserAsync(role);
    }

    public async Task<SecurityResult> AddRoleForUser(Teammate user, string role)
    {
        return await userAccountProcessor.AddRoleForUser(user, role);
    }


    public JwtSecurityToken GetToken(List<Claim> claims)
    {
        return tokenProvider.GetToken(claims);
    }

    public async Task<string> GetLoggedUsersGUIDAsync()
    {
        return await userAccountProcessor.GetCurrentUserIdAsync();
    }
}