using DataAccess.Enums;
using DataAccess.Models.Projects;

namespace DataAccess.Models.Factories;


public static class FeatureFactory
{
    public static Feature Create(string guid, string name, string description, ExtendedStatus state, int? points, string? projectGUID, string createdBy, DateTime createdOn)
    {
        return Create(guid, name, description, state, points, projectGUID, createdBy, createdOn, createdBy, createdOn);
    }
    
    public static Feature Create(string guid, string name, string description, ExtendedStatus state, int? points, string? projectGUID, string createdBy, DateTime createdOn, string lastUpdatedBy, DateTime lastUpdateOn)
    {
        var newFeature =  new Feature
        {
            GUID = guid,
            Name = name,
            Description = description,
            State = state,
            Points = points,
            ProjectGUID = projectGUID,
            CreatedBy = createdBy,
            CreatedOn = createdOn,
            LastUpdatedBy = lastUpdatedBy,
            LastUpdateOn = lastUpdateOn,
        };
        
        HistoryTableHelper.PopulateMissingValues(newFeature, createdBy, createdOn, createdBy, createdOn);

        return newFeature;
    }
}