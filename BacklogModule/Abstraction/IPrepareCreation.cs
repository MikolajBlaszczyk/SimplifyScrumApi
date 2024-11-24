namespace BacklogModule.Abstraction;

public interface IPrepareCreation<T>
{
     void Prepare(T entity);
}