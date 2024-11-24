using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Model.User;
using DataAccess.Models.Projects;

namespace DataAccess.Accessors;

public class UserHierarchyAccessor(SimplifyAppDbContext dbContext) : IUserHierarchyAccessor
{
    public Team? GetTeam(string teamGUID)
    {
        return dbContext
                .Teams
                .FirstOrDefault(t => t.GUID == teamGUID);
    }

    public Project? GetProjectByTeam(string teamGUID)
    {
        var projects = dbContext
            .Projects
            .Where(p => p.TeamGUID == teamGUID);

        if (projects.Count() > 1)
            throw new Exception();

        return projects.FirstOrDefault();
    }

    public Team AddTeam(Team newTeam)
    {
        if (string.IsNullOrEmpty(newTeam.GUID))
            newTeam.GUID = Guid.NewGuid().ToString();
        
        dbContext.Teams.Add(newTeam);
        dbContext.SaveChanges();
        return newTeam;
    }

    public List<Team> GetAllTeams()
    {
        return dbContext.Teams.ToList();
    }

    public Team GetTeamByGUID(string teamGUID)
    {
        return dbContext
            .Teams
            .FirstOrDefault(t => t.GUID == teamGUID);
    }
}