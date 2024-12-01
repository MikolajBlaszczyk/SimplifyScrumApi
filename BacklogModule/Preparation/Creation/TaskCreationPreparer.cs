using BacklogModule.Abstraction;
using Task = DataAccess.Models.Projects.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;


namespace BacklogModule.Preparation.Creation;


public class TaskCreationPreparer : IPrepareCreation<Task>
{
    public void Prepare(Task entity)
    {
        //TODO: Test after UT implementation
        //This probably can be change to 0
        var correct = TaskFactory.Create(
            entity.Name,
            entity.State,
            entity.FeatureGUID,
            entity.Assignee,
            entity.CreatedBy,
            entity.CreatedOn, 
            entity.LastUpdatedBy,
            entity.LastUpdateOn
            );

        entity.ID = correct.ID;
    }
}