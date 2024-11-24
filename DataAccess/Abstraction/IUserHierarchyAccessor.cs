using DataAccess.Model.User;
using DataAccess.Models.Projects;

namespace DataAccess.Abstraction;

public interface IUserHierarchyAccessor
{
    Project? GetProjectByTeam(string teamGUID);
    Team AddTeam(Team newTeam);
    List<Team> GetAllTeams();
    Team GetTeamByGUID(string teamGUID);
}