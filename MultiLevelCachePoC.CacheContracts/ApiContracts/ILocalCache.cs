using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{   
    public interface ILocalCache<T> where T : ICacheableEntity
    {        
        void Insert(T cacheItem, bool withSync = false);
        
        T Get(string identifier, bool withSync = false);
        
        void Delete(string identifier, bool withSync = false);

        void ClearCache();
    }
}
