namespace BacklogModule.Abstraction;

public interface IEntityPreparerFactory
{
    IPrepareCreation<T> GetCreationPreparer<T>();
}