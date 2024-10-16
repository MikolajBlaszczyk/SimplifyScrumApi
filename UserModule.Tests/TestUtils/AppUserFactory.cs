using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public static class AppUserFactory
{
    public static SimpleUserModel CreateAppUser( string username, string password, string nickname = "", ScrumRole? role = null, string email = "")
    {
        return new SimpleUserModel(username, password, email, nickname, role);
    }
}