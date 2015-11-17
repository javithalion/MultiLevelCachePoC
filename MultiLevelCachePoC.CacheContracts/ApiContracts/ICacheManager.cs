using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{
    public interface ICacheManager
    {
        void Insert(CacheableEntity item, bool withSync = false);

        CacheableEntity Get(string identifier, bool withSync = false);
        
        void Delete(string identifier, bool withSync = false);
       
        void ClearCache();
    }
}
