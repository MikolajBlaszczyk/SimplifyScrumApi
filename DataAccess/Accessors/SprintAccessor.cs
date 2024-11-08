using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Models.Projects;

namespace DataAccess.Accessors;

public class SprintAccessor(SimplifyAppDbContext dbContext) : ISprintAccessor
{
    public Sprint? GetCurrentSprintInfoByProject(string projectGUID)
    {
        return dbContext
            .Sprints
            .FirstOrDefault(s => s.ProjectGUID == projectGUID);
    }

    public Sprint? GetSprintByGuid(string sprintGUID)
    {
        return dbContext
            .Sprints
            .FirstOrDefault(s => s.GUID == sprintGUID);
    }
}