using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageSecurity
{
    Task<SecurityResult> Login(SimpleUserModel userModel);
    Task<SecurityResult> Logout();
    Task<SecurityResult> SignIn(SimpleUserModel userModel);
    Task<SecurityResult> Delete();
    JwtSecurityToken GetToken(List<Claim> claims);
    Task<string> GetLoggedUsersGuid();
}