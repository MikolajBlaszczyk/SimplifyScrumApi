namespace SimplifyFramework.Cache;

public interface ICacheKeyValuePairs
{
    public void Set<T>(string key, T value);
    
    public bool Get<T>(string key, out T value);
    
    public void Remove(string key);
}