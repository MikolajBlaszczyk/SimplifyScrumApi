using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction.Storage;

public interface IFeatureStorage
{
    List<Feature> GetFeatureByProjectGUID(string projectGUID);

    Feature GetFeatureByGUID(string featureGUID);
    
    Feature AddFeature(Feature feature);

    Feature UpdateFeature(Feature feature);

    Feature DeleteFeature(Feature feature);
}