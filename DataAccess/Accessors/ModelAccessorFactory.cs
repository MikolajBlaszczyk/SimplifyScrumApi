using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Accessors;

public class ModelAccessorFactory(IServiceProvider serviceProvider): ICreateAccessors
{
 
    public IAccessor<T> Create<T>() where T : class
    {
        return serviceProvider.GetRequiredService<IAccessor<T>>();
    }

    private SimplifyAppDbContext? _dbContext = null;
    public SimplifyAppDbContext DbContext
    {
        get
        {
            if (_dbContext == null)
            {
                _dbContext = serviceProvider.GetRequiredService<SimplifyAppDbContext>();
            }
            
            return _dbContext;
        }
    }
}