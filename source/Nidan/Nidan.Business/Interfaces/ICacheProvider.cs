namespace Nidan.Business.Interfaces
{
    public interface ICacheProvider
    {
        object Get(string key);
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Invalidate(string key);
        
    }
}
