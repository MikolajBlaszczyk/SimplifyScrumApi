using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class SprintStorage(ICreateAccessors factory, ILogger<Sprint> logger) : ISprintStorage
{
    public Sprint GetSprintInfoByProjectGUID(string projectGUID)
    {
        var dbContext = factory.DbContext;
        
        var sprint = dbContext
            .Sprints
            .FirstOrDefault(s => s.ProjectGUID == projectGUID);
        if (sprint is null)
        {
            logger.LogError($"Sprint with project guid {projectGUID} does not exsists");
            throw new AccessorException($"Sprint with project guid {projectGUID} does not exsists");
        }

        return sprint;
    }
}