using DataAccess.Models.Projects;

namespace DataAccess.Abstraction.Storage;

public interface IProjectStorage
{
    List<Project> GetAllProjects();

    Project GetProjectByGUID(string projectGUID);

    Project AddProject(Project project);

    Project UpdateProject(Project project);

    Project DeleteProject(Project project);
}