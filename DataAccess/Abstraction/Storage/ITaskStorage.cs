using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Abstraction;

public interface ITaskStorage
{

    List<Task> GetTasksByFeatureGUID(string featureGUID);
    
    Task GetTaskByID(int taskID);
    
    Task AddTask(Task task);
    
    Task UpdateTask(Task task);

    Task DeleteTask(Task delete);
}