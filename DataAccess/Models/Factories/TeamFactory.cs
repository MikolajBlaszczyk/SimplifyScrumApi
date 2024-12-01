using DataAccess.Model.User;

namespace DataAccess.Models.Factories;

public static class TeamFactory
{
    public static Team Create(string GUID, string Name, string ManagerGUID)
    {
        return new Team
        {
            GUID = GUID,
            Name = Name,
            ManagerGUID = ManagerGUID
        };
    } 
}