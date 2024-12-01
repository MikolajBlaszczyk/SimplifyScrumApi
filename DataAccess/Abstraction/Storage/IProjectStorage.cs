using DataAccess.Models.Projects;

namespace DataAccess.Abstraction.Storage;

public interface IProjectStorage
{
    Task<List<Project>> GetAllProjects();
    Task<Project> GetProjectByGUID(string projectGUID);
    Task<Project> AddProject(Project project);
    Task<Project> UpdateProject(Project project);
    Task<Project> DeleteProject(Project project);
}