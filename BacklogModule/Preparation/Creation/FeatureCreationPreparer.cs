using BacklogModule.Abstraction;
using DataAccess.Models.Projects;

namespace BacklogModule.Preparation.Creation;

public class FeatureCreationPreparer: IPrepareCreation<Feature>
{
    public void Prepare(Feature entity)
    {
        // if (string.IsNullOrEmpty(entity.OD))
        //     entity.OD = Guid.NewGuid().ToString();
    }
}