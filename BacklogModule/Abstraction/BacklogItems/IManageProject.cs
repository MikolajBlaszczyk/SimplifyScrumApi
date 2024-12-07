using BacklogModule.Models;
using BacklogModule.Utils.Results;

namespace BacklogModule.Abstraction.BacklogItems;

public interface IManageProject
{
    Task<BacklogResult> GetAllProjectsForTeam(string teamGUID);
    
    Task<BacklogResult> GetProjectByGuid(string projectGUID);
    
    Task<BacklogResult> AddProject(ProjectRecord project);
    
    Task<BacklogResult> DeleteProject(string projectGUID);
    
    Task<BacklogResult> UpdateProject(ProjectRecord project);
    Task<BacklogResult> GetProjectByTeamGuid(string teamGuid);
}