using UserModule.Records;
using UserModule.Security.Models;

namespace UserModule.Abstraction;

public interface IManageSecurity
{
    Task<SecurityResult> Login(AppUser user);
    Task<SecurityResult> Logout();
    Task<SecurityResult> SignIn(AppUser user);
    Task<SecurityResult> Delete();
}