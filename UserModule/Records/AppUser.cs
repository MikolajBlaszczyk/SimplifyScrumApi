using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public record AppUser(
    string Username,
    SecureString Password,
    string Email,
    string Nickname = "",
    ScrumRole? Role = null) : IDisposable
{
    public void Dispose()
    {
        Password.Dispose();
    }
}