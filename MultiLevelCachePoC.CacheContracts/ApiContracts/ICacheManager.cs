using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.ServiceModel;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{
    [ServiceKnownType(typeof(CacheableEntity))]    
    [ServiceContract]
    public interface ICacheManager
    {
        [OperationContract]
        void Insert(CacheableEntity item, bool withSync = false);

        [OperationContract]
        CacheableEntity Get(string identifier, bool withSync = false);

        [OperationContract]
        void Delete(string identifier, bool withSync = false);

        [OperationContract]
        void ClearCache();
    }
}
