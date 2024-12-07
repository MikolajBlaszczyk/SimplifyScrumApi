using BacklogModule.Models;
using BacklogModule.Utils.Results;

namespace BacklogModule.Abstraction.BacklogItems;

public interface IManageBacklog
{
    Task<BacklogResult> GetSprintInfoWithProjectGuid(string projectGuid);
    
    Task<BacklogResult> GetTeamProjects(string teamGuid);
    Task<BacklogResult> UpdateProject(ProjectRecord record);
    Task<BacklogResult> AddProject(ProjectRecord record);
    Task<BacklogResult> DeleteProject(string projectGuid);
    Task<BacklogResult> GetProjectByGuid(string projectGuid);
    Task<BacklogResult> GetProjectByTeam(string teamGuid);
    
    
    Task<BacklogResult> GetProjectFeatures(string projectGuid);
    Task<BacklogResult> GetFeatureByGuid(string featureGuid);
    Task<BacklogResult> AddFeature(FeatureRecord record);
    Task<BacklogResult> DeleteFeature(string featureGuid);
    Task<BacklogResult> UpdateFeature(FeatureRecord record);
    
    Task<BacklogResult> GetFeatureTasks(string featureGuid);
    Task<BacklogResult> GetTaskById(int taskId);
    Task<BacklogResult> AddTask(TaskRecord record);
    Task<BacklogResult> DeleteTask(int taskId);
    Task<BacklogResult> UpdateTask(TaskRecord record);
}