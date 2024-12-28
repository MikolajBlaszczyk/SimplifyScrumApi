using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record ProjectRecord(
    string GUID,
    string Name,
    string? Description,
    StandardStatus State,
    string TeamGUID,
    string CreatedBy,
    DateTime CreatedOn,
    string LastUpdatedBy,
    DateTime LastUpdatedOn,
    bool IsActive 
)
{
    public static ProjectRecord Create( string GUID,
        string Name,
        string? Description,
        StandardStatus State,
        string Team,
        string CreatedBy,
        DateTime CreatedOn)
    {
        return Create(GUID,
            Name,
            Description,
            State,
            Team,
            CreatedBy,
            CreatedOn,
            CreatedBy, 
            CreatedOn);
    }
    
    public static ProjectRecord Create( string GUID,
        string Name,
        string? Description,
        StandardStatus State,
        string Team,
        string CreatedBy,
        DateTime CreatedOn,
        string LastUpdatedBy,
        DateTime LastUpdatedOn,
        bool isActive = false)
    {
        return new ProjectRecord(
            GUID,
            Name,
            Description,
            State,
            Team,
            CreatedBy,
            CreatedOn,
            LastUpdatedBy,
            LastUpdatedOn,
            isActive);
    }
    
    public static  implicit operator Project(ProjectRecord? record)
    {
        return ProjectFactory
            .Create(
                record.GUID,
                record.Name,
                record.Description,
                record.State,
                record.TeamGUID,
                record.CreatedBy,
                record.CreatedOn,
                record.LastUpdatedBy,
                record.LastUpdatedOn,
                record.IsActive
            );
    }
    
    public static  implicit operator ProjectRecord(Project model)
    {
        return  Create(
            model.GUID,
            model.Name,
            model.Description,
            model.State,
            model.TeamGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn,
            model.IsActive
        );
    }
}