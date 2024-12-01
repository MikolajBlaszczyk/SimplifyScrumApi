using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction;

public interface ITaskStorage
{
    Task<List<Task>> GetTasksByFeatureGUID(string featureGUID);

    Task<Task> GetTaskByID(int taskID);

    Task<Task> AddTask(Task task);

    Task<Task> UpdateTask(Task task);

    Task<Task> DeleteTask(Task delete);
}