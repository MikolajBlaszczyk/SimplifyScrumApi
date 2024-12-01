using DataAccess.Abstraction.Accessors.Factories;

namespace DataAccess.Abstraction;

public interface IAccessor<T>  where T : class
{
    Task<T?> Add(T model);
    Task<List<T>?> GetAllByPKs(IList<object> values);
    Task<T?> GetByPK(object? value);
    Task<List<T>?> GetAll();
    Task<T?> Delete(T model);
    Task<T?> Update(T model);
}