using BacklogModule.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogModule.Preparation;

public class EntityPreparerFactory(IServiceProvider serviceProvider) : IEntityPreparerFactory
{
    
    public IPrepareCreation<T> GetCreationPreparer<T>()
    {
        return serviceProvider.GetRequiredService<IPrepareCreation<T>>();
    }
}