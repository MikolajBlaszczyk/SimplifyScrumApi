using DataAccess.Enums;
using DataAccess.Models.Projects;

namespace DataAccess.Models.Factories;

public static class ProjectFactory
{
    public static Project Create(
        string GUID,
        string name,
        string? description,
        StandardStatus state, 
        string teamGUID,
        string createdBy,
        DateTime createdOn)
    {
        return Create(GUID, name, description, state, teamGUID, createdBy, createdOn, createdBy, createdOn);
    }
    
    public static Project Create(
        string GUID,
        string name,
        string? description,
        StandardStatus state, 
        string teamGUID,
        string createdBy,
        DateTime createdOn,
        string lastUpdatedBy,
        DateTime lastUpdateOn,
        bool isActive = false)
    {
        var newProject =  new Project()
        {
            GUID = GUID,
            Name = name,
            Description = description,
            State = state,
            TeamGUID = teamGUID,
            IsActive = isActive
        };
        
        HistoryTableHelper.PopulateMissingValues(newProject, createdBy,  createdOn, lastUpdatedBy, lastUpdateOn);

        return newProject;
    }
}