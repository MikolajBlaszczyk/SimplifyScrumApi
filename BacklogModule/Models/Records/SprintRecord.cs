using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record SprintRecord(
    string GUID,
    string Name,
    string Goal,
    int Iteration,
    DateTime End,
    string ProjectGUID,
    string CreatedBy,
    DateTime CreatedOn,
    string LastUpdateBy,
    DateTime LastUpdateOn,
    bool IsFinished)
{
    public static SprintRecord Create(string GUID,
        string Name,
        string Goal,
        int Iteration,
        DateTime End,
        string ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn)
    {
        return Create(
            GUID,
            Name,
            Goal,
            Iteration,
            End,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            CreatedBy,
            CreatedOn,
            false);
    }
    
    public static SprintRecord Create(string GUID,
        string Name,
        string Goal,
        int Iteration,
        DateTime End,
        string ProjectGUID,
        string CreatedBy,
        DateTime CreatedOn,
        string LastUpdateBy,
        DateTime LastUpdateOn,
        bool IsFinished)
    {
        return new SprintRecord(GUID,
            Name,
            Goal,
            Iteration,
            End,
            ProjectGUID,
            CreatedBy,
            CreatedOn,
            LastUpdateBy,
            LastUpdateOn,
            IsFinished);
    }

    public static  implicit operator Sprint(SprintRecord record)
    {
        return SprintFactory
            .Create(
                record.GUID,
                record.Name,
                record.Goal,
                record.Iteration,
                record.End,
                record.ProjectGUID,
                record.CreatedBy,
                record.CreatedOn,
                record.LastUpdateBy,
                record.LastUpdateOn,
                record.IsFinished
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
            model.LastUpdateOn,
            model.IsFinished
        );
    }
}