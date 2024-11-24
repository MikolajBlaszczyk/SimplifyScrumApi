using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record SprintRecord(
    string GUID,
    string Name,
    string Goal,
    int IterationNumber,
    DateTime End,
    string ProjectGUID,
    string CreatedBy,
    DateTime CreatedOn,
    string LastUpdateBy,
    DateTime LastUpdateOn)
{
    public static SprintRecord Create(string GUID,
        string Name,
        string Goal,
        int IterationNumber,
        DateTime End,
        string ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn)
    {
        return Create(
            GUID,
            Name,
            Goal,
            IterationNumber,
            End,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            CreatedBy,
            CreatedOn);
    }
    
    public static SprintRecord Create(string GUID,
        string Name,
        string Goal,
        int IterationNumber,
        DateTime End,
        string ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn,
        string LastUpdateBy,
        DateTime LastUpdateOn)
    {
        return new SprintRecord(GUID,
            Name,
            Goal,
            IterationNumber,
            End,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            LastUpdateBy,
            LastUpdateOn);
    }

    public static  implicit operator Sprint(SprintRecord record)
    {
        return SprintFactory
            .Create(
                record.GUID,
                record.Name,
                record.Goal,
                record.IterationNumber,
                record.End,
                record.ProjectGUID,
                record.CreatedBy,
                record.CreatedOn,
                record.LastUpdateBy,
                record.LastUpdateOn
            );
    }
    
    public static  implicit operator SprintRecord(Sprint model)
    {
        return  Create(
            model.GUID,
            model.Name,
            model.Goal,
            model.Iteration,
            model.End,
            model.ProjectGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn
        );
    }
}