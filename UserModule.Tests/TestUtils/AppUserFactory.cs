using System.Security;
using DataAccess.Enums;
using UserModule.Informations;

namespace UserModule.Records;

public static class AppUserFactory
{
    public static SimpleUserModel CreateAppUser( string username, string password, string teamGuid = "", string nickname = "", ScrumRole? role = null, string email = "")
    {
        return new SimpleUserModel(username, password, email, teamGuid, nickname, role);
    }
}