using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public interface IPersistenceEngine
    {
        void Persist(CacheableEntity value);
        void Remove(CacheableEntity value);

        IEnumerable<CacheableEntity> Load();
    }
}
