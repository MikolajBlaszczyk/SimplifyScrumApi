using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction;

public interface IProjectItemsAccessor
{
    Task<List<Project>> GetAllProjects();
    Task<List<Feature>> GetFeatureByProjectGUID(string projectGUID);
    Task<List<Task>> GetTasksByFeatureGUID(string featureGUID);

    Task<Project> GetProjectByGUID(string projectGUID);
    Task<Feature> GetFeatureByGUID(string featureGUID);
    Task<Task> GetTaskByID(int taskID);
    
    Task<Project> AddProject(Project project);
    Task<Feature> AddFeature(Feature feature);
    Task<Task> AddTask(Task task);

    Task<Project> UpdateProject(Project project);
    Task<Feature> UpdateFeature(Feature feature);
    Task<Task> UpdateTask(Task task);

    Task<Project> DeleteProject(string projectGUID);
    Task<Feature> DeleteFeature(string featureGUID);
    Task<Task> DeleteTask(int taskID);
}