using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils;
using BacklogModule.Utils.Exceptions;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;
using DataAccess.Abstraction.Tables;
using DataAccess.Models.Projects;

namespace BacklogModule.BacklogManagement;

public class ProjectManager(IProjectStorage projectStorage, IEntityPreparerFactory preparerFactory) : IManageProject
{
    public async Task<BacklogResult> GetAllProjectsForTeam(string teamGUID)
    {
        var projects =  projectStorage.GetAllProjects();
            
        var teamProjects = projects.Where(p => p.TeamGUID == teamGUID);
        List<ProjectRecord> result = new List<ProjectRecord>();
        foreach (var project in teamProjects)
        {
            ProjectRecord record = project;
            result.Add(record);
        }

        return result;
    }

    public async Task<BacklogResult> GetProjectByGuid(string projectGUID)
    {
        ProjectRecord record  = projectStorage.GetProjectByGUID(projectGUID);
        return record;
    }

    public async Task<BacklogResult> AddProject(ProjectRecord record)
    {
        if (record is null)
            throw new BacklogException();
        
        Project project = record;
        var projectPreparer =  preparerFactory.GetCreationPreparer<Project>();
        projectPreparer.Prepare(project);

        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(project);
        
        ProjectRecord result = projectStorage.AddProject(project);
        return result;
    }

    public async Task<BacklogResult> DeleteProject(string projectGUID)
    {
        var projectToDelete = projectStorage.GetProjectByGUID(projectGUID);
        ProjectRecord result = projectStorage.DeleteProject(projectToDelete);
        return result;
    }

    public async Task<BacklogResult> UpdateProject(ProjectRecord record)
    {
        Project project = record;
        
        ProjectRecord result = projectStorage.UpdateProject(project);
        
        return result;
    }
}