using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Context;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class FeatureStorage(ICreateAccessors factory, ILogger<FeatureStorage> logger): IFeatureStorage
{
    private IAccessor<Feature> _featureAccessor = factory.Create<Feature>();
    
    public List<Feature> GetFeatureByProjectGUID(string projectGUID)
    {
        var dbContext = factory.DbContext;
        var tasks = dbContext
            .Features
            .Where(t => t.ProjectGUID == projectGUID)
            .ToList();

        return tasks;
    }

    public Feature GetFeatureByGUID(string featureGUID)
    {
        var feautre = _featureAccessor.GetByPK(featureGUID);
        if (feautre is null)
        {
            logger.LogError($"Feature with guid {feautre} does not exists");
            throw new AccessorException($"Feature with guid {feautre} does not exists");
        }

        return feautre;
    }

    public Feature AddFeature(Feature feature)
    {
        var addedFeature = _featureAccessor.Add(feature);
        if (addedFeature is null)
        {
            logger.LogError("Cannot add feature");
            throw new AccessorException("Cannot add feature");
        }

        return addedFeature;
    }

    public Feature UpdateFeature(Feature feature)
    {
        var updatedFeature = _featureAccessor.Update(feature);
        if (updatedFeature is null)
        {
            logger.LogError("Cannot update feature");
            throw new AccessorException("Cannot update feature");
        }

        return updatedFeature;
    }

    public Feature DeleteFeature(Feature feature)
    {
        var deletedFeature = _featureAccessor.Delete(feature);
        if (deletedFeature is null)
        {
            logger.LogError("Cannot delete feature");
            throw new AccessorException("Cannot delete feature");
        }

        return deletedFeature;
    }
}