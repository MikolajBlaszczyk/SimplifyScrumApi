using DataAccess.Enums;
using Task = DataAccess.Models.Projects.Task;
namespace DataAccess.Models.Factories;

public static class TaskFactory
{
    
    public static Task Create(int id, string name, SimpleStatus state, string featureGUID, string? assignee, string createdBy, DateTime createdOn)
    {
        return Create(id, name, state, featureGUID, assignee, createdBy, createdOn, createdBy, createdOn);
    }
    
    public static Task Create(int id, string name, SimpleStatus state, string featureGUID, string? assignee, string createdBy, DateTime createdOn, string lastUpdatedBy, DateTime lastUpdateOn)
    {
        var newTask =  new Task
        {
            ID = id,
            Name = name,
            State = state,
            FeatureGUID = featureGUID,
            Assignee = assignee,
          
        };
        
        HistoryTableHelper.PopulateMissingValues(newTask, createdBy, createdOn, lastUpdatedBy, lastUpdateOn);

        return newTask;
    }
    
    public static Task Create( string name, SimpleStatus state, string featureGUID, string? assignee, string createdBy, DateTime createdOn, string lastUpdatedBy, DateTime lastUpdateOn)
    {
        var newTask =  new Task
        {
            Name = name,
            State = state,
            FeatureGUID = featureGUID,
            Assignee = assignee,
          
        };
        
        HistoryTableHelper.PopulateMissingValues(newTask, createdBy, createdOn, lastUpdatedBy, lastUpdateOn);

        return newTask;
    }
}