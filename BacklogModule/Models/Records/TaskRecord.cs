using DataAccess.Enums;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;
using Task = DataAccess.Models.Projects.Task;

namespace BacklogModule.Models;

public record TaskRecord(
    int Id,
    string Name,
    SimpleStatus State,
    string FeatureGUID,
    string? Assignee,
    string CreatedBy,
    DateTime CreatedOn,
    string LastUpdatedBy, 
    DateTime LastUpdateOn)
{
    public static TaskRecord Create(
        int id,
        string name,
        SimpleStatus state,
        string featureGUID,
        string? assignee,
        string createdBy,
        DateTime createdOn)
    {
        return Create(id, name, state, featureGUID, assignee, createdBy, createdOn, createdBy, createdOn);
    }
    
    public static TaskRecord Create(
        int id,
        string name,
        SimpleStatus state,
        string featureGUID,
        string? assignee,
        string createdBy,
        DateTime createdOn,
        string lastUpdatedBy,
        DateTime lastUpdateOn)
    {
        return new TaskRecord(id, name, state, featureGUID, assignee, createdBy, createdOn, lastUpdatedBy, lastUpdateOn);
    }
    
    public static  implicit operator Task(TaskRecord record)
    {
        return TaskFactory
            .Create(
                record.Id,
                record.Name,
                record.State,
                record.FeatureGUID,
                record.Assignee,
                record.CreatedBy,
                record.CreatedOn,
                record.LastUpdatedBy,
                record.LastUpdateOn
            );
    }
    
    public static  implicit operator TaskRecord(Task model)
    {
        return  Create(
            model.ID,
            model.Name,
            model.State,
            model.FeatureGUID,
            model.Assignee,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn
        );
    }
}