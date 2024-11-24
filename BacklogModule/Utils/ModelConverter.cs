using BacklogModule.Models;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;

namespace BacklogModule.Utils;

public static class ModelConverter
{

    #region Sprint
    public static SprintRecord ConvertSprintModelToRecord(Sprint model)
    {
        return  SprintRecord.Create(
            model.GUID,
            model.Name,
            model.Goal,
            model.Iteration,
            model.End,
            model.ProjectGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn
        );
    }

    #endregion

    #region Project
    public static ProjectRecord ConvertProjectModelToRecord(Project model)
    {
        return ProjectRecord.Create(
            model.GUID,
            model.Name,
            model.Description,
            model.State,
            model.TeamGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn);
    }

    public static Project ConvertProjectRecordToModel(ProjectRecord record)
    {
        return ProjectFactory.Create(
            record.GUID,
            record.Name,
            record.Description,
            record.State,
            record.TeamGUID,
            record.CreatedBy,
            record.CreatedOn,
            record.LastUpdatedBy,
            record.LastUpdatedOn
            );
    }
    #endregion

    #region Feature

    public static FeatureRecord ConvertFeatureModelToRecord(Feature model)
    {
        return FeatureRecord.Create(
            model.GUID,
            model.Name,
            model.Description,
            model.State,
            model.Points,
            model.ProjectGUID,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn);
    }

    public static Feature ConvertFeatureRecordToModel(FeatureRecord record)
    {
        return FeatureFactory.Create(
            record.GUID,
            record.Name,
            record.Description,
            record.State,
            record.Points,
            record.ProjectGUID,
            record.CreatedBy,
            record.CreatedOn,
            record.LastUpdatedBy,
            record.LastUpdateOn
        );
    }

    #endregion
    
    public static TaskRecord ConvertTaskModelToRecord(Task model)
    {
        return TaskRecord.Create(
            model.ID,
            model.Name,
            model.State,
            model.FeatureGUID,
            model.Assignee,
            model.CreatedBy,
            model.CreatedOn,
            model.LastUpdatedBy,
            model.LastUpdateOn);
    }

    public static Task ConvertTaskRecordToModel(TaskRecord record)
    {
        return TaskFactory.Create(
            record.Id,
            record.Name,
            record.State,
            record.FeatureGUID,
            record.Assignee,
            record.CreatedBy,
            record.CreatedOn,
            record.LastUpdatedBy,
            record.LastUpdateOn
        );
    }
}