using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils;
using BacklogModule.Utils.Exceptions;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;
using DataAccess.Abstraction.Tables;
using DataAccess.Models.Projects;

namespace BacklogModule.BacklogManagement;

public class FeatureManager(IFeatureStorage featureStorage, IEntityPreparerFactory preparerFactory) : IManageFeature
{
    public async Task<BacklogResult> GetAllFeaturesByProjectGUID(string projectGuid)
    {
        var features = await featureStorage.GetFeatureByProjectGUID(projectGuid);

        List<FeatureRecord> result = new List<FeatureRecord>();
        foreach (var feature in features)
        {
            FeatureRecord record = feature;
            result.Add(record);
        }

        return result;
    }

    public async Task<BacklogResult> GetFeatureByGuid(string featureGUID)
    {
        FeatureRecord feature = await featureStorage.GetFeatureByGUID(featureGUID);
        return feature;
    }

    public async Task<BacklogResult> AddFeature(FeatureRecord record)
    {
        if (record is null)
            throw new BacklogException();
        
        Feature feature = record;
        var featurePreparer = preparerFactory.GetCreationPreparer<Feature>();
        featurePreparer.Prepare(feature);

        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(feature);
        
        FeatureRecord result = await featureStorage.AddFeature(feature);
        return result;
    }

    public async Task<BacklogResult> DeleteFeature(string featureGUID)
    {
        var featureToDelete = await featureStorage.GetFeatureByGUID(featureGUID);
        FeatureRecord result = await featureStorage.DeleteFeature(featureToDelete);
        return result;
    }

    public async Task<BacklogResult> UpdateFeature(FeatureRecord record)
    {
        Feature feature = record;
        FeatureRecord result = await featureStorage.UpdateFeature(feature);
        return result; 
    }
}