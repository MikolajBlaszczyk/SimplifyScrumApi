using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace DataAccess.Accessors;

public class ModelAccessor<T>(SimplifyAppDbContext dbContext, ILogger<ModelAccessor<T>> logger):  IAccessor<T> where T : class, IAccessorTable
{
    
    public async Task<T?> Add(T model)
    {
        try
        {
            var createdEntry  = await dbContext.Set<T>().AddAsync(model);
            await dbContext.SaveChangesAsync();
            return createdEntry.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error adding {model.GetType()} to database | Exception --> {e.Message}");
            return null;
        }
    }

    public async Task<List<T>?> GetAllByPKs(IList<object> values)
    {
        if (values.Any() == false)
        {
            logger.LogError("There is no primary keys to search");
            return null;
        }

        var entityType = GetEntityByModel(typeof(T));
        
        var pkProperty = GetEntityPrimaryKey(entityType);

        List<T> result = new List<T>();
        IQueryable<T> query = dbContext.Set<T>();
        //Only support for one key
        foreach (var value in values)
        { 
            var searchedValue = await query
                .FirstOrDefaultAsync(e => 
                    EF.Property<object>(e, pkProperty).Equals(value));

            if (searchedValue is null)
            {
                logger.LogInformation($"There are no {typeof(T).Name} with PK {value}");
                continue;
            }
            
            result.Add(searchedValue);
        }

        return result;
    }

    public async Task<T?> GetByPK(object? value)
    {
        if (value == null)
        {
            logger.LogError("There are no primary key to search");
            return null;
        }

        var entityType = GetEntityByModel(typeof(T));

        var pkProperty = GetEntityPrimaryKey(entityType);

        IQueryable<T> query = dbContext.Set<T>();

        var searchedValue = await query
            .FirstOrDefaultAsync(e =>
                EF.Property<object>(e, pkProperty).Equals(value));

        if (searchedValue is null)
            logger.LogInformation($"There are no {typeof(T).Name} with PK {value}");

        return searchedValue;
    }

    public async Task<List<T>?> GetAll()
    {
        IQueryable<T> query = dbContext.Set<T>();
        
        return await query.ToListAsync();
    }

    public async Task<T?> Delete(T model)
    {
        try
        {
            var existingEntity = await dbContext.Set<T>().FindAsync(model.GetPrimaryKey());
            if (existingEntity == null)
            {
                logger.LogError($"Entity of type {typeof(T).Name} with PK {model.GetPrimaryKey()} not found.");
                return null;
            }
            
            
            var deletedEntry  = dbContext.Set<T>().Remove(existingEntity);
            await dbContext.SaveChangesAsync();
            return deletedEntry.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error deleting {model.GetType()} to database | Exception --> {e.Message}");
            return null;
        }
    }

    public async Task<T?> Update(T model)
    {
        try
        {
            var existingEntity = await dbContext.Set<T>().FindAsync(model.GetPrimaryKey());
            if (existingEntity == null)
            {
                logger.LogError($"Entity of type {typeof(T).Name} with PK {model.GetPrimaryKey()} not found.");
                return null;
            }
            
            dbContext.Entry(existingEntity).CurrentValues.SetValues(model);
            await dbContext.SaveChangesAsync();
            return model;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error updating {model.GetType()} to database | Exception --> {e.Message}");
            return null;
        }
    }

 

    private string GetEntityPrimaryKey(IEntityType entityType)
    {
        var pkProperty = entityType
            .FindPrimaryKey()?
            .Properties
            .Select(p => p.Name);
        
        if (pkProperty is null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} is not a model in DbContext. It doesn't have any PK.");

        if (pkProperty.Count() != 1)
            throw new AccessorException("More than one pk is not supported");

        return pkProperty.FirstOrDefault();
    }

    private IEntityType GetEntityByModel(Type model)
    {
        var entityType = dbContext.Model.FindEntityType(typeof(T));
        if (entityType == null)
            throw new InvalidOperationException($"Entity type {typeof(T).Name} is not registered in DbContext.");

        return entityType;
    }
}