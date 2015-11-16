using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public interface IPersistenceEngine<T> where T : ICacheableEntity
    {
        IEnumerable<T> Load();
        void Remove(T value);
        void Persist(T value);
    }
}
