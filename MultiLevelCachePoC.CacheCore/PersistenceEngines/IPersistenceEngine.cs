using MultiLevelCachePoC.CacheCore.EntityContracts;
using System;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public interface IPersistenceEngine : IDisposable
    {
        IEnumerable<CacheableEntity> Load();
        void Persist(CacheableEntity value);
        void Remove(CacheableEntity value);        
    }
}
