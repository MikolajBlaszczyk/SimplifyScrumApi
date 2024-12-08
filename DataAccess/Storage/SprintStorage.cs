using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Model.ConnectionTables;
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
            logger.LogWarning($"Sprint with project guid {projectGUID} does not exsists");
            return null;
        }

        return sprint;
    }

    public async Task<Sprint> GetSprintByGuid(string sprintGuid)
    {
        var accessors = factory.Create<Sprint>();
        
        var sprint = await accessors.GetByPK(sprintGuid);
        if (sprint is null)
        {
            logger.LogWarning($"Sprint with guid {sprint} does not exists");
            throw new AccessorException($"Sprint with guid {sprint} does not exists");
        }

        return sprint;
    }

    public async Task<Sprint> AddSprint(Sprint sprint)
    {
        var accessor = factory.Create<Sprint>();

        var addedSprint  = await accessor.Add(sprint);
        if (addedSprint is null)
        {
            logger.LogError("Cannot add sprint");
            throw new AccessorException("Cannot add sprint");
        }

        return addedSprint;
    }

    public async Task<Sprint> UpdateSprint(Sprint sprint)
    {
        var accessor = factory.Create<Sprint>();
        
        var updatedSprint = await accessor.Update(sprint);
        if (updatedSprint is null)
        {
            logger.LogError("Cannot update sprint");
            throw new AccessorException("Cannot update sprint");
        }

        return updatedSprint;
    }

    public async System.Threading.Tasks.Task LinkSprintWithFeatures(Sprint sprint, List<string> featureGuids)
    {
        var dbContext = factory.DbContext;

        foreach (var guid in featureGuids)
        {
            var link = new SprintFeatures { SprintGUID = sprint.GUID, FeatureGUID = guid };
            await dbContext.SprintFeatures.AddAsync(link);
        }

        await dbContext.SaveChangesAsync();
    }
}