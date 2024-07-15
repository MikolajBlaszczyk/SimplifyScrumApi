using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public static class AppUserFactory
{
    public static AppUser CreateAppUser( string username, string password, string nickname = "", ScrumRole? role = null, string email = "")
    {
        var pwd = new SecureString();

        foreach (char letter in password)
            pwd.AppendChar(letter);

        return new AppUser(username, pwd, email, nickname, role);
    }
}