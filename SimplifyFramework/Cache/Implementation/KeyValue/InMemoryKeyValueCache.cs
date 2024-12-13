using Microsoft.Extensions.Caching.Memory;

namespace SimplifyFramework.Cache.Implementation.KeyValue;

public class InMemoryKeyValueCache(IMemoryCache cache) : ICacheKeyValuePairs
{
    public void Set<T>(string key, T value)
    {
        cache.Set(key, value);
    }

    public bool Get<T>(string key, out T value)
    {
        return cache.TryGetValue(key, out value);
    }

    public void Remove(string key)
    {
        cache.Remove(key);
    }
}