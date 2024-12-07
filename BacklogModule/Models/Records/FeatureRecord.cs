using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record FeatureRecord(
        string GUID,
        string Name, 
        string Description,
        ExtendedStatus State,
        int? Points,
        string? ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn,
        string LastUpdatedBy,
        DateTime LastUpdateOn,
        bool AssignedToSprint,
        RefinementState RefinementState)
{
    public static FeatureRecord Create(
        string GUID,
        string Name, 
        string Description,
        ExtendedStatus State,
        int? Points,
        string? ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn)
    {
        return Create(
            GUID,
            Name,
            Description,
            State,
            Points,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            CreatedBy,
            CreatedOn,
            false, 
            RefinementState.NotReady
        );
    }
    
    public static FeatureRecord Create(
        string GUID,
        string Name, 
        string Description,
        ExtendedStatus State,
        int? Points,
        string? ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn,
        string LastUpdatedBy,
        DateTime LastUpdateOn,
        bool AssignedToSprint,
        RefinementState RefinementState)
    {
        return new FeatureRecord(
            GUID,
            Name,
            Description,
            State,
            Points,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            LastUpdatedBy,
            LastUpdateOn,
            AssignedToSprint,
            RefinementState
        );
    }
    
    public static  implicit operator Feature(FeatureRecord record)
    {
        return FeatureFactory
            .Create(
                record.GUID,
                record.Name,
                record.Description,
                record.State,
                record.Points,
                record.ProjectGUID,
                record.CreatedBy,
                record.CreatedOn,
                record.LastUpdatedBy,
                record.LastUpdateOn,
                record.AssignedToSprint,
                record.RefinementState
            );
    }
    
    public static  implicit operator FeatureRecord(Feature model)
    {
        return  Create(
            model.GUID,
            model.Name,
            model.Description,
            model.State,
            model.Points,
            model.ProjectGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn,
            model.AssignedToSprint,
            model.RefinementState
        );
    }
}