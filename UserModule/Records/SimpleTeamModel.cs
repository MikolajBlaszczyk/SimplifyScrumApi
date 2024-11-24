using DataAccess.Model.User;

namespace UserModule.Records;

public class SimpleTeamModel(string guid, string name, string managerGUID)
{
    public string GUID { get; } = guid;
    public string Name { get; } = name;
    public string ManagerGUID { get; } = managerGUID;

    public static implicit operator Team(SimpleTeamModel model)
    {
        return new Team
        {
            GUID = model.GUID,
            Name = model.Name,
            ManagerGUID = model.ManagerGUID,
        };
    }
    public static implicit operator SimpleTeamModel(Team team)
    {
        return new SimpleTeamModel(team.GUID, team.Name, team.ManagerGUID);
    }
}