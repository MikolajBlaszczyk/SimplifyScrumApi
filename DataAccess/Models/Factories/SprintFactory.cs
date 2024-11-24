using DataAccess.Abstraction.Tables;
using DataAccess.Models.Projects;

namespace DataAccess.Models.Factories;

public static class SprintFactory
{
    public static Sprint Create(string guid, string name, string goal, int iteration, DateTime end, string ProjectGUID, string createdBy, DateTime createdOn)
    {
        return Create(guid, name, goal, iteration, end, ProjectGUID, createdBy, createdOn, createdBy , createdOn);
    }
    
    public static Sprint Create(string guid, string name, string goal, int iteration, DateTime end, string ProjectGUID, string createdBy, DateTime createdOn, string lastUpdatedBy, DateTime lastUpdatedOn)
    {
        var newSprint =  new Sprint
        {
            GUID = guid,
            Name = name,
            Goal = goal,
            Iteration = iteration,
            End = end,
            ProjectGUID = ProjectGUID,
        };
        
        HistoryTableHelper.PopulateMissingValues(newSprint, createdBy, createdOn, lastUpdatedBy, lastUpdatedOn);

        return newSprint;
    }
}