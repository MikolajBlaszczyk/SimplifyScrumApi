using BacklogModule.Models;
using BacklogModule.Models.Results;

namespace BacklogModule.Abstraction;

public interface IManageProjectItems
{
    Task<List<ProjectRecord>> GetAllProjectsForTeam(string teamGUID);
    Task<List<FeatureRecord>> GetAllFeaturesByProjectGUID(string projectGuid);
    Task<List<TaskRecord>> GetAllTasksByFeatureGUID(string featureGUID);

    Task<BacklogResult> GetProjectByGuid(string projectGUID);
    Task<BacklogResult> GetFeatureByGUID(string featureGUID);
    
    Task<BacklogResult> getTaskByID(int taskId);
    
    Task AddProject(ProjectRecord project);
    Task AddFeature(FeatureRecord feature);
    Task AddTask(TaskRecord task);

    Task DeleteProject(string projectGUID);
    Task DeleteFeature(string featureGUID);
    Task DeleteTask(int taskID);

    Task UpdateProject(ProjectRecord project);
    Task UpdateFeature(FeatureRecord feature);
    Task UpdateTask(TaskRecord task);
}