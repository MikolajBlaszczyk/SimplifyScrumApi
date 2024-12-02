using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Context;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class FeatureStorage(ICreateAccessors factory, ILogger<FeatureStorage> logger): IFeatureStorage
{
    private IAccessor<Feature> _featureAccessor = factory.Create<Feature>();
    public async Task<List<Feature>> GetFeatureByProjectGUID(string projectGUID)
    {
        var dbContext = factory.DbContext;
        var tasks = await dbContext
            .Features
            .Where(t => t.ProjectGUID == projectGUID)
            .ToListAsync();
        
        return tasks;
    }

    public async Task<Feature> GetFeatureByGUID(string featureGUID)
    {
        var feautre = await _featureAccessor.GetByPK(featureGUID);
        if (feautre is null)
        {
            logger.LogWarning($"Feature with guid {feautre} does not exists");
            throw new AccessorException($"Feature with guid {feautre} does not exists");
        }

        return feautre;
    }
    
    public async Task<Feature> AddFeature(Feature feature)
    {
        var addedFeature = await _featureAccessor.Add(feature);
        if (addedFeature is null)
        {
            logger.LogError("Cannot add feature");
            throw new AccessorException("Cannot add feature");
        }

        return addedFeature;
    }

    public async Task<Feature> UpdateFeature(Feature feature)
    {
        var updatedFeature = await _featureAccessor.Update(feature);
        if (updatedFeature is null)
        {
            logger.LogError("Cannot update feature");
            throw new AccessorException("Cannot update feature");
        }

        return updatedFeature;
    }

   public async Task<Feature> DeleteFeature(Feature feature)
    {
        var deletedFeature = await _featureAccessor.Delete(feature);
        if (deletedFeature is null)
        {
            logger.LogError("Cannot delete feature");
            throw new AccessorException("Cannot delete feature");
        }

        return deletedFeature;
    }
}