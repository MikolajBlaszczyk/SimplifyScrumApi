using DataAccess.Model.User;
using UserModule.Records;

namespace UserModule.Security.Models.Converters;

public class UserModelConverter
{
    public Teammate ConvertToTeammate(AppUser user)
    {
        return new Teammate
        {
            Nickname = user.Nickname,
            UserName = user.Username,
            Email = user.Email,
            ScrumRole = user.Role
        };
    }

    public AppUser ConvertToAppUser(Teammate teammate)
    {
        return new AppUser(
            teammate.UserName,
            "",
            teammate.Email,
            teammate.Nickname,
            teammate.ScrumRole
            );
    }
}