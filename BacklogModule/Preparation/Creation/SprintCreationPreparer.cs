using BacklogModule.Abstraction;
using DataAccess.Models.Projects;

namespace BacklogModule.Preparation.Creation;

public class SprintCreationPreparer : IPrepareCreation<Sprint>
{
    public void Prepare(Sprint entity)
    {
        if (string.IsNullOrEmpty(entity.GUID))
            entity.GUID = Guid.NewGuid().ToString();
    }
}