using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public static class AppUserFactory
{
    public static AppUser CreateAppUser( string username, string password, string nickname = "", ScrumRole? role = null, string email = "")
    {
        return new AppUser(username, password, email, nickname, role);
    }
}