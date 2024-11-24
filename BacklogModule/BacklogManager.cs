using BacklogModule.Abstraction;
using BacklogModule.Models;
using BacklogModule.Models.Results;
using BacklogModule.Utils;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;
using DataAccess.Models.Projects;
using Task = System.Threading.Tasks.Task;

namespace BacklogModule;

public class BacklogManager(ISprintAccessor sprintAccessor, IProjectItemsAccessor projectItemsAccessor, IEntityPreparerFactory preparerFactory) : IManageSprint, IManageProjectItems
{
    public BacklogResult GetSprintInfoForProject(string projectGUID)
    {
        try
        {
            var sprintModel = sprintAccessor.GetSprintInfoByProjectGUID(projectGUID);

            if (sprintModel is null)
                throw new BacklogException("There is no sprint that belongs to this Project");

            SprintRecord record = sprintModel;
            return record;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<List<ProjectRecord>> GetAllProjectsForTeam(string teamGUID)
    {
        var projects =  await projectItemsAccessor.GetAllProjects();

        var teamsProjects = projects.Where(p => p.TeamGUID == teamGUID);
        
        return teamsProjects.Select(ModelConverter.ConvertProjectModelToRecord).ToList();
    }

    public async Task<List<FeatureRecord>> GetAllFeaturesByProjectGUID(string projectGuid)
    {
        var features = await projectItemsAccessor.GetFeatureByProjectGUID(projectGuid);

        return features.Select(ModelConverter.ConvertFeatureModelToRecord).ToList();
    }

    public async Task<List<TaskRecord>> GetAllTasksByFeatureGUID(string featureGUID)
    {
        var tasks = await projectItemsAccessor.GetTasksByFeatureGUID(featureGUID);

        return tasks.Select(ModelConverter.ConvertTaskModelToRecord).ToList();
    }

    public async Task<BacklogResult> GetProjectByGuid(string projectGUID)
    {
        try
        {
            var project = await projectItemsAccessor.GetProjectByGUID(projectGUID);

            if (projectGUID is null)
                throw new BacklogException("There is no sprint that belongs to this Project");

            ProjectRecord record = project;
            return record;
        }
        catch (Exception ex)
        {
            return ex;
        }
    
    }

    public async Task<BacklogResult> GetFeatureByGUID(string featureGUID)
    {
        try
        {
            FeatureRecord feature = await projectItemsAccessor.GetFeatureByGUID(featureGUID);
            return feature;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<BacklogResult> getTaskByID(int taskId)
    {
        try
        {
            TaskRecord task = await projectItemsAccessor.GetTaskByID(taskId);
            return task;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task AddProject(ProjectRecord record)
    {
        var project = ModelConverter.ConvertProjectRecordToModel(record);
        
        var projectPreparer =  preparerFactory.GetCreationPreparer<Project>();
        projectPreparer.Prepare(project);

        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(project);
        
        await projectItemsAccessor.AddProject(project);
    }

    public async Task AddFeature(FeatureRecord record)
    {
        var feature = ModelConverter.ConvertFeatureRecordToModel(record);

        var featurePreparer = preparerFactory.GetCreationPreparer<Feature>();
        featurePreparer.Prepare(feature);

        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(feature);
        
        await projectItemsAccessor.AddFeature(feature);
    }

    public async Task AddTask(TaskRecord record)
    {
        var task = ModelConverter.ConvertTaskRecordToModel(record);

        var taskPreparer = preparerFactory.GetCreationPreparer<DataAccess.Models.Projects.Task>();
        taskPreparer.Prepare(task);
        
        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(task);
        
        await projectItemsAccessor.AddTask(task);
    }

    public async Task DeleteProject(string projectGUID)
    {
        await projectItemsAccessor.DeleteProject(projectGUID);
    }

    public async Task DeleteFeature(string featureGUID)
    {
        await projectItemsAccessor.DeleteFeature(featureGUID);
    }

    public async Task DeleteTask(int taskID)
    {
        await projectItemsAccessor.DeleteTask(taskID);
    }

    public async Task UpdateProject(ProjectRecord record)
    {
        var project = ModelConverter.ConvertProjectRecordToModel(record);
        await projectItemsAccessor.UpdateProject(project);
    }

    public async Task UpdateFeature(FeatureRecord record)
    {
        var feature = ModelConverter.ConvertFeatureRecordToModel(record);
        await projectItemsAccessor.UpdateFeature(feature);
    }

    public async Task UpdateTask(TaskRecord record)
    {
        var task = ModelConverter.ConvertTaskRecordToModel(record);
        await projectItemsAccessor.UpdateTask(task);
    }
}