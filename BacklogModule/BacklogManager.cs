using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;

namespace BacklogModule;

public class BacklogManager(IManageSprint sprintManager, IManageFeature featureManager, IManageProject projectManager, IManageTask taskManager) : IManageBacklog
{
    #region sprint
    
    public async Task<BacklogResult> GetSprintInfoWithProjectGuid(string projectGuid)
    {
        try
        {
            return await sprintManager.GetSprintInfoByProjectGUID(projectGuid);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    #endregion
    
    
    #region project
    public async Task<BacklogResult> GetTeamProjects(string teamGuid)
    {
        try
        {
            return await projectManager.GetAllProjectsForTeam(teamGuid);
        }
        catch (Exception e)
        {
            return e;
        }
       
    }
    
    public async Task<BacklogResult> UpdateProject(ProjectRecord record)
    {
        try
        {
            return await projectManager.UpdateProject(record);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    
    public async Task<BacklogResult> AddProject(ProjectRecord record)
    {
        try
        {
            return await projectManager.AddProject(record);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<BacklogResult> DeleteProject(string projectGuid)
    {
        try
        {
            return await projectManager.DeleteProject(projectGuid);
        }
        catch (Exception e)
        {
            return e;
        }
      
    }
    
    public async Task<BacklogResult> GetProjectByGuid(string projectGuid)
    {
        try
        {
            return await projectManager.GetProjectByGuid(projectGuid);
        }
        catch (Exception ex)
        {
            return ex;
        }
    
    }
    #endregion


    #region feature
    public async Task<BacklogResult> GetProjectFeatures(string projectGuid)
    {
        try
        {
            return await featureManager.GetAllFeaturesByProjectGUID(projectGuid);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<BacklogResult> GetFeatureByGuid(string featureGuid)
    {
        try
        {
            return await featureManager.GetFeatureByGuid(featureGuid);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    public async Task<BacklogResult> AddFeature(FeatureRecord record)
    {
        try
        {
            return await featureManager.AddFeature(record);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<BacklogResult> DeleteFeature(string featureGuid)
    {
        try
        {
            return await featureManager.DeleteFeature(featureGuid);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<BacklogResult> UpdateFeature(FeatureRecord record)
    {
        try
        {
            return await featureManager.UpdateFeature(record);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    #endregion


    #region Task
    public async Task<BacklogResult> GetFeatureTasks(string featureGuid)
    {
        try
        {
            return await taskManager.GetAllTasksByFeatureGUID(featureGuid);
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<BacklogResult> GetTaskById(int taskId)
    {
        try
        {
            return await taskManager.GetTaskById(taskId);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    public async Task<BacklogResult> AddTask(TaskRecord record)
    {
        try
        {
            return await taskManager.AddTask(record);
        }
        catch (Exception e)
        {
            return e;
        }
      
    }

    public async Task<BacklogResult> DeleteTask(int taskId)
    {
        try
        {
            return await taskManager.DeleteTask(taskId);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    
    public async Task<BacklogResult> UpdateTask(TaskRecord record)
    {
        try
        {
            return await taskManager.UpdateTask(record);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    #endregion
}