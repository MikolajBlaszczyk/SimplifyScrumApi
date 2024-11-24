using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Models.Projects;

namespace DataAccess.Accessors;

public class SprintAccessor(SimplifyAppDbContext dbContext) : ISprintAccessor
{
    public Sprint? GetSprintInfoByProjectGUID(string projectGUID)
    {
        return dbContext
            .Sprints
            .FirstOrDefault(s => s.ProjectGUID == projectGUID);
    }
}