using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Utils;
using Microsoft.Extensions.Logging;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Storage;

public class TaskStorage(ICreateAccessors factory, ILogger<TaskStorage> logger): ITaskStorage
{
    private IAccessor<Task> _taskAccessor = factory.Create<Task>(); 
    
    public List<Task> GetTasksByFeatureGUID(string featureGUID)
    {
        var dbContext = factory.DbContext;
        var tasks = dbContext
            .Tasks
            .Where(t => t.FeatureGUID == featureGUID)
            .ToList();

        return tasks;
    }

    public Task GetTaskByID(int taskID)
    {
        var task = _taskAccessor.GetByPK(taskID);
        if (task is null)
        {
            logger.LogError($"Task with id {taskID} does not exists");
            throw new AccessorException($"Task with id {taskID} does not exists");
        }

        return task;
    }

    public Task AddTask(Task task)
    {
        var addedTask = _taskAccessor.Add(task);
        if (addedTask is null)
        {
            logger.LogError("Cannot add task");
            throw new AccessorException("Cannot add task");
        }
        
        return addedTask;
    }

    public Task UpdateTask(Task task)
    {
        var updatedTask = _taskAccessor.Update(task);
        if (updatedTask is null)
        {
            logger.LogError("Cannot update task");
            throw new AccessorException("Cannot update task");
        }
        
        return updatedTask;
    }

    public Task DeleteTask(Task task)
    {
        var deleteTask = _taskAccessor.Update(task);
        if (deleteTask is null)
        {
            logger.LogError("Cannot delete task");
            throw new AccessorException("Cannot delete task");
        }
        
        return deleteTask;
    }
}