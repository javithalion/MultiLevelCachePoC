using MultiLevelCachePoC.CacheCore.EntityContracts;
using System;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheCore.ApiContracts
{
    [ServiceKnownType(typeof(CacheableEntity))]
    [ServiceContract]
    public interface ICacheManager
    {
        [OperationContract]
        void Set(CacheableEntity item, SyncMode syncMode = SyncMode.NoSync);

        [OperationContract]
        CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.NoSync);

        [OperationContract]
        void Delete(string identifier, SyncMode syncMode = SyncMode.NoSync);

        [OperationContract]
        void ClearCache();

        [OperationContract]
        long ElementsCount();
    }
}
