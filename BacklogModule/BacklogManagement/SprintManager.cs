using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils.Exceptions;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;
using DataAccess.Abstraction.Tables;
using DataAccess.Enums;
using DataAccess.Models.Projects;

namespace BacklogModule.BacklogManagement;

public class SprintManager(ISprintStorage sprintStorage, IFeatureStorage featureStorage, IEntityPreparerFactory preparerFactory, ISprintNoteStorage sprintNoteStorage) : IManageSprint
{
    public async Task<BacklogResult> GetSprintInfoByProjectGUID(string projectGUID)
    {
        try
        {
            var model = sprintStorage.GetSprintInfoByProjectGUID(projectGUID);
            if (model is null)
                return BacklogResult.SuccessWithoutData();

            SprintRecord record = model;

            if (record.IsFinished)
                return BacklogResult.SuccessWithoutData();;
            return record;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<BacklogResult> PlanSprint(PlanSprintRecord record)
    {
        try
        {
            Sprint sprint = record.Sprint;

            var sprintPreparer = preparerFactory.GetCreationPreparer<Sprint>();
            sprintPreparer.Prepare(sprint);
            
            var preparer =preparerFactory.GetCreationPreparer<HistoryTable>();
            preparer.Prepare(sprint);
            
            var addedSprint = await sprintStorage.AddSprint(sprint);
            
            await sprintStorage.LinkSprintWithFeatures(addedSprint, record.FeatureGUIDs);

            var features = await featureStorage.GetFeatureByProjectGUID(addedSprint.ProjectGUID);
            foreach (var feature in features)
            {
                if (record.FeatureGUIDs.Contains(feature.GUID))
                {
                    feature.AssignedToSprint = true;
                    feature.State = ExtendedStatus.ReadyForImplementation;
                    await featureStorage.UpdateFeature(feature);
                }
            }

            SprintRecord result = addedSprint;
            return result;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<BacklogResult> RateSprint(SprintNoteRecord record)
    {
        try
        {
            SprintNote note = record;
            await sprintNoteStorage.AddSprintNotes(note);
       
            var sprint =  await sprintStorage.GetSprintByGuid(record.SprintGUID);
            sprint.IsFinished = true;
            SprintRecord updatedSprint = await sprintStorage.UpdateSprint(sprint);
            
            return updatedSprint;
        }
        catch (Exception e)
        {
            
            return e;
        }
    }

    public async Task<BacklogResult> GetActiveItemsForSprint(string projectGUID)
    {
        try
        {
            var sprintResult = await GetSprintInfoByProjectGUID(projectGUID);
            var sprint = sprintResult.Data as SprintRecord;

            var featuresWithTasks = await featureStorage.GetFeaturesWithTasksBySprintGUID(sprint.GUID);
            
            List<FeatureRecord> featureRecords = new List<FeatureRecord>();
            foreach (var feature in featuresWithTasks)
            {
                FeatureRecord featureRecord = feature;
                List<TaskRecord> taskRecords = new List<TaskRecord>();
                foreach (var task in feature.Tasks)
                {
                    TaskRecord taskRecord = task;
                    taskRecords.Add(taskRecord);
                }

                featureRecord = featureRecord with { Tasks = taskRecords };
                featureRecords.Add(featureRecord);
            }

            return featureRecords;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}