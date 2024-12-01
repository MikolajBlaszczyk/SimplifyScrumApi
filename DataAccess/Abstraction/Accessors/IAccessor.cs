using DataAccess.Abstraction.Accessors.Factories;

namespace DataAccess.Abstraction;

public interface IAccessor<T>  where T : class
{
    T? Add(T model);
    List<T>? GetAllByPKs(IList<object> values);
    T? GetByPK(object? value);
    List<T>? GetAll();
    T? Delete(T model);
    T? Update(T model);
}