using DataAccess.Context;

namespace DataAccess.Abstraction.Accessors.Factories;

public interface ICreateAccessors
{
    IAccessor<T> Create<T>() where T: class;
    SimplifyAppDbContext DbContext { get; }
}