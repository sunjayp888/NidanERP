using System;
using System.Runtime.Caching;
using Nidan.Business.Interfaces;

namespace Nidan.Business
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public object Get(string key)
        {
            return Cache[key];
        }

        public T Get<T>(string key)
        {
            var t = Cache[key];

            if (t is T)
                return (T)t;
            else
            {
                try
                {
                    return (T)Convert.ChangeType(t, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }
        }

        public void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.UtcNow.Date + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return Get(key) != null;
        }

        public void Invalidate(string key)
        {
            if (IsSet(key))
                Cache.Remove(key);
        }

       
    }
}
