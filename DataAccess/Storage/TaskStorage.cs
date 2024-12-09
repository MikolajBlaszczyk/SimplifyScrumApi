using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Storage;

public class TaskStorage(ICreateAccessors factory, ILogger<TaskStorage> logger) : ITaskStorage
{
    private IAccessor<Task> _taskAccessor = factory.Create<Task>();

    public async Task<List<Task>> GetTasksByFeatureGUID(string featureGUID)
    {
        var dbContext = factory.DbContext;
        var tasks = await dbContext
            .Tasks
            .Where(t => t.FeatureGUID == featureGUID)
            .ToListAsync();

        return tasks;
    }

    public async Task<Task> GetTaskByID(int taskID)
    {
        var task = await _taskAccessor.GetByPK(taskID);
        if (task is null)
        {
            logger.LogError($"Task with id {taskID} does not exists");
            throw new AccessorException($"Task with id {taskID} does not exists");
        }

        return task;
    }

    public async Task<Task> AddTask(Task task)
    {
        var addedTask = await _taskAccessor.Add(task);
        if (addedTask is null)
        {
            logger.LogError("Cannot add task");
            throw new AccessorException("Cannot add task");
        }

        return addedTask;
    }

    public async Task<Task> UpdateTask(Task task)
    {
        var updatedTask = await _taskAccessor.Update(task);
        if (updatedTask is null)
        {
            logger.LogError("Cannot update task");
            throw new AccessorException("Cannot update task");
        }

        return updatedTask;
    }

    public async Task<Task> DeleteTask(Task task)
    {
        var deleteTask = await _taskAccessor.Delete(task);
        if (deleteTask is null)
        {
            logger.LogError("Cannot delete task");
            throw new AccessorException("Cannot delete task");
        }

        return deleteTask;
    }

  
}