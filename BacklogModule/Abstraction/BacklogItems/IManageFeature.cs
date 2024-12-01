using BacklogModule.Models;
using BacklogModule.Utils.Results;

namespace BacklogModule.Abstraction.BacklogItems;

public interface IManageFeature
{
    Task<BacklogResult> GetAllFeaturesByProjectGUID(string projectGuid);
    Task<BacklogResult> GetFeatureByGuid(string featureGUID);
    Task<BacklogResult> AddFeature(FeatureRecord feature);
    Task<BacklogResult> DeleteFeature(string featureGUID);
    Task<BacklogResult> UpdateFeature(FeatureRecord feature);
}