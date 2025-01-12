
using DataAccess.Enums;
using DataAccess.Model.User;
using UserModule.Informations;

namespace UserModule.Records;

public record SimpleUserModel(
    string Username,
    string Password,
    string Email,
    string TeamGuid = "",
    string Nickname = "",
    ScrumRole? Role = null,
    string Id = "",
    bool NewUser = false)
{
    public string? SystemRole { get; set; }
    public static implicit operator Teammate(SimpleUserModel userModel)
    {
        return new Teammate
        {
            Nickname = userModel.Nickname,
            UserName = userModel.Username,
            Email = userModel.Email,
            ScrumRole = userModel.Role,
            NewUser = userModel.NewUser,
        };
    }
    public static implicit operator SimpleUserModel(Teammate teammate)
    {
        return new SimpleUserModel(
            teammate.UserName,
            "",
            teammate.Email,
            teammate.TeamGUID,
            teammate.Nickname,
            teammate.ScrumRole,
            teammate.Id,
            teammate.NewUser
        );
    }

   
}
    