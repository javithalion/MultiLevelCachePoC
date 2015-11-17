using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{
    [ServiceKnownType(typeof(CacheableEntity))]
    [ServiceContract]
    public interface ICacheManager
    {
        [OperationContract]
        void Insert(CacheableEntity item, SyncMode syncMode = SyncMode.WithoutSync);

        [OperationContract]
        CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.WithoutSync);

        [OperationContract]
        void Delete(string identifier, SyncMode syncMode = SyncMode.WithoutSync);

        [OperationContract]
        void ClearCache();
    }
}
