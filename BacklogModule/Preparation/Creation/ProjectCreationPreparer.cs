using BacklogModule.Abstraction;
using DataAccess.Models.Projects;

namespace BacklogModule.Preparation.Creation;

public class ProjectCreationPreparer : IPrepareCreation<Project>
{
    public void Prepare(Project entity)
    {
        if (string.IsNullOrEmpty(entity.GUID))
            entity.GUID = Guid.NewGuid().ToString();
    }
    
}