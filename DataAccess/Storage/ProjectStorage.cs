using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;

namespace DataAccess.Storage;

public class ProjectStorage(ICreateAccessors factory, ILogger<ProjectStorage> logger) : IProjectStorage
{
    private IAccessor<Project> _projectAccessor = factory.Create<Project>();

    public async Task<List<Project>> GetAllProjects()
    {
        var projects = await _projectAccessor.GetAll();
        if (projects is null)
        {
            logger.LogError("Cannot retrieve projects");
            throw new AccessorException("Cannot retrieve projects");
        }

        return projects;
    }

    public async Task<Project> GetProjectByGUID(string projectGUID)
    {
        var project = await _projectAccessor.GetByPK(projectGUID);
        if (project is null)
        {
            logger.LogError($"Project with guid {projectGUID} does not exists");
            throw new AccessorException($"Project with guid {projectGUID} does not exists");
        }

        return project;
    }

    public async Task<Project> AddProject(Project project)
    {
        var addedProject = await _projectAccessor.Add(project);
        if (addedProject is null)
        {
            logger.LogError("Cannot add project");
            throw new AccessorException("Cannot add project");
        }

        return addedProject;
    }

    public async Task<Project> UpdateProject(Project project)
    {
        var updateProject = await _projectAccessor.Update(project);
        if (updateProject is null)
        {
            logger.LogError("Cannot update project");
            throw new AccessorException("Cannot update project");
        }

        return updateProject;
    }

    public async Task<Project> DeleteProject(Project project)
    {
        var deleteProject = await _projectAccessor.Update(project);
        if (deleteProject is null)
        {
            logger.LogError("Cannot delete project");
            throw new AccessorException("Cannot delete project");
        }

        return deleteProject;
    }
}