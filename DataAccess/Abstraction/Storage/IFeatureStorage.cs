using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction.Storage;

public interface IFeatureStorage
{
    Task<List<Feature>> GetFeatureByProjectGUID(string projectGUID);

    Task<Feature> GetFeatureByGUID(string featureGUID);
    Task<List<Feature>> GetFeaturesWithTasksBySprintGUID(string sprintGUID);
    
    Task<Feature> AddFeature(Feature feature);

    Task<Feature> UpdateFeature(Feature feature);

    Task<Feature> DeleteFeature(Feature feature);
}