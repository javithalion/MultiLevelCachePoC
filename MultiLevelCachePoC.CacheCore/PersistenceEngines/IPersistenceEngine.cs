using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public interface IPersistenceEngine<T> where T : CacheableEntity
    {
        void Persist(T value);
        void Remove(T value);

        IEnumerable<T> Load();
    }
}
