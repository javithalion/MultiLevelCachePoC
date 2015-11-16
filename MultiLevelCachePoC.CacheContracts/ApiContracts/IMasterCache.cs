using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{
    [ServiceKnownType(typeof(CacheableEntity))]
    [ServiceKnownType(typeof(Engine))]
    [ServiceContract]
    public interface IMasterCache
    {
        [OperationContract]
        void Insert(CacheableEntity cacheItem, bool withSync = false);

        [OperationContract]
        CacheableEntity Get(string identifier, bool withSync = false);

        [OperationContract]
        void Delete(string identifier, bool withSync = false);

        [OperationContract]
        void ClearCache();
    }
}
