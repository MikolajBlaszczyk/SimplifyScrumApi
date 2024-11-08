using DataAccess.Model.User;
using UserModule.Records;

namespace UserModule.Security.Models.Converters;

public class UserModelConverter
{
    public Teammate ConvertToTeammate(SimpleUserModel userModel)
    {
        return new Teammate
        {
            Nickname = userModel.Nickname,
            UserName = userModel.Username,
            Email = userModel.Email,
            ScrumRole = userModel.Role
        };
    }

    public SimpleUserModel ConvertToAppUser(Teammate teammate)
    {
        return new SimpleUserModel(
            teammate.UserName,
            "",
            teammate.Email,
            teammate.TeamGUID,
            teammate.Nickname,
            teammate.ScrumRole,
            teammate.Id
            );
    }
}