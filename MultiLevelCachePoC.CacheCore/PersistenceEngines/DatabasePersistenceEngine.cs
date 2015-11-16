using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public class DatabasePersistenceEngine<T> : IPersistenceEngine<T> where T : class, ICacheableEntity
    {
        public IEnumerable<T> Load()
        {
            throw new NotImplementedException();
        }

        public void Persist(T value)
        {
            throw new NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new NotImplementedException();
        }
    }
}
