using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public class DatabasePersistenceEngine : IPersistenceEngine
    {
        public IEnumerable<CacheableEntity> Load()
        {
            throw new NotImplementedException();
        }

        public void Persist(CacheableEntity value)
        {
            throw new NotImplementedException();
        }

        public void Remove(CacheableEntity value)
        {
            throw new NotImplementedException();
        }
    }
}
