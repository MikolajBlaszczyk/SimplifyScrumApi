using BacklogModule.Abstraction;
using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils;
using BacklogModule.Utils.Exceptions;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Tables;

namespace BacklogModule.BacklogManagement;

public class TaskManager(ITaskStorage taskStorage, IEntityPreparerFactory preparerFactory) : IManageTask
{
    public async Task<BacklogResult> GetAllTasksByFeatureGUID(string featureGUID)
    {
        var tasks = await taskStorage.GetTasksByFeatureGUID(featureGUID);

        List<TaskRecord> result = new List<TaskRecord>();
        foreach (var task in tasks)
        {
            TaskRecord record = task;
            result.Add(record);
        }
        
        return  result;
    }

    public async Task<BacklogResult> GetTaskById(int taskId)
    {
        TaskRecord task = await taskStorage.GetTaskByID(taskId);
        return task;
    }

    public async Task<BacklogResult> AddTask(TaskRecord record)
    {
        if (record is null)
            throw new BacklogException();

        
        DataAccess.Models.Projects.Task task = record;

        var taskPreparer = preparerFactory.GetCreationPreparer<DataAccess.Models.Projects.Task>();
        taskPreparer.Prepare(task);
        
        var historyPreparer = preparerFactory.GetCreationPreparer<HistoryTable>();
        historyPreparer.Prepare(task);
        
        TaskRecord result = await taskStorage.AddTask(task);
        return result;
    }

    public async Task<BacklogResult> DeleteTask(int taskID)
    {
        var taskToDelete = await taskStorage.GetTaskByID(taskID);
        TaskRecord result = await taskStorage.DeleteTask(taskToDelete);
        return result;
    }

    public async Task<BacklogResult> UpdateTask(TaskRecord record)
    {
        DataAccess.Models.Projects.Task task = record;
        TaskRecord result = await taskStorage.UpdateTask(task);
        return result;
    }
}