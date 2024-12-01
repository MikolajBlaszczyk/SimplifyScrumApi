using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class ProjectStorage(ICreateAccessors factory, ILogger<ProjectStorage> logger): IProjectStorage
{
    private IAccessor<Project> _projectAccessor = factory.Create<Project>();
    
    public List<Project> GetAllProjects()
    {
        var projects = _projectAccessor.GetAll();
        if (projects is null)
        {
            logger.LogError("Cannot retrieve projects");
            throw new AccessorException("Cannot retrieve projects");
        }

        return projects;
    }

    public Project GetProjectByGUID(string projectGUID)
    {
        var project = _projectAccessor.GetByPK(projectGUID);
        if (project is null)
        {
            logger.LogError($"Project with guid {projectGUID} does not exists");
            throw new AccessorException($"Project with guid {projectGUID} does not exists");
        }

        return project;
    }

    public Project AddProject(Project project)
    {
        var addedProject = _projectAccessor.Add(project);
        if (addedProject is null)
        {
            logger.LogError("Cannot add project");
            throw new AccessorException("Cannot add project");
        }

        return addedProject;
    }

    public Project UpdateProject(Project project)
    {
        var updateProject = _projectAccessor.Update(project);
        if (updateProject is null)
        {
            logger.LogError("Cannot update project");
            throw new AccessorException("Cannot update project");
        }

        return updateProject;
    }

    public Project DeleteProject(Project project)
    {
        var deleteProject = _projectAccessor.Update(project);
        if (deleteProject is null)
        {
            logger.LogError("Cannot delete project");
            throw new AccessorException("Cannot delete project");
        }

        return deleteProject;
    }
}