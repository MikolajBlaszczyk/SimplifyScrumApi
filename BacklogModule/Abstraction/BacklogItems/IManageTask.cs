using BacklogModule.Models;
using BacklogModule.Utils.Results;

namespace BacklogModule.Abstraction.BacklogItems;

public interface IManageTask
{
    Task<BacklogResult> GetAllTasksByFeatureGUID(string featureGUID);
    
    Task<BacklogResult> GetTaskById(int taskId);
    
    Task<BacklogResult> AddTask(TaskRecord task);
    
    Task<BacklogResult> DeleteTask(int taskId);

    Task<BacklogResult> UpdateTask(TaskRecord task);
}