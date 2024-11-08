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
}