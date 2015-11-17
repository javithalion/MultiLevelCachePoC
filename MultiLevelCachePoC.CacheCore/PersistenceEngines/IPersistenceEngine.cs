using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public interface IPersistenceEngine
    {
        IEnumerable<CacheableEntity> Load();
        void Persist(CacheableEntity value);
        void Remove(CacheableEntity value);        
    }
}
