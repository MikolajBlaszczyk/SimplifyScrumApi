using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccess.Model.User;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageSecurity
{
    Task<SecurityResult> LoginAsync(SimpleUserModel userModel);
    Task<SecurityResult> LogoutAsync();
    Task<SecurityResult> SignInAsync(SimpleUserModel userModel);
    Task<SecurityResult> DeleteAsync();
    Task<SecurityResult> AddRoleAsyncForCurrentUser(string role);
    Task<SecurityResult> AddRoleForUser(Teammate user, string role);
    JwtSecurityToken GetToken(List<Claim> claims);
    Task<string> GetLoggedUsersGUIDAsync();
}