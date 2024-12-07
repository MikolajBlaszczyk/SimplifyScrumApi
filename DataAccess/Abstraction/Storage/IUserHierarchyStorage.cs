using DataAccess.Model.User;
using DataAccess.Models.Projects;

namespace DataAccess.Abstraction;

public interface IUserHierarchyStorage
{
    Task<Project> GetActiveProjectByTeam(string teamGUID);
    Task<Team> GetTeam(string teamGUID);
    Task<Team> AddTeam(Team newTeam);
    Task<List<Team>> GetAllTeams();
    Task<Team> GetTeamByGUID(string teamGUID);
}