using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using MultiLevelCachePoC.CacheCore.UpperTierCache;
using System;
using System.Runtime.Caching;

namespace MultiLevelCachePoC.CacheCore.Core
{
    public class CacheManager<T> : ILocalCache<T> where T : CacheableEntity
    {
        private readonly string _cacheName;
        private MemoryCache _cacheInfraestructure;
        private readonly IPersistenceEngine<T> _persistenceEngine;
        private readonly ILocalCacheOf_Engine _upperTierCacheClient;

        public CacheManager(IPersistenceEngine<T> engine = null)
        {
            _cacheName = typeof(T).Name;
            _cacheInfraestructure = new MemoryCache(_cacheName);
            _persistenceEngine = engine;
            _upperTierCacheClient = new LocalCacheOf_EngineClient();

            LoadInitialCacheContent();
        }

        private void LoadInitialCacheContent()
        {
            if (_persistenceEngine == null) return;

            foreach (var item in _persistenceEngine.Load())
                Insert(item);
        }

        public void ClearCache()
        {
            //TODO :: Make this thread safe
            _cacheInfraestructure.Dispose();
            _cacheInfraestructure = new MemoryCache(_cacheName);
        }

        public void Insert(T cacheItem, bool withSync = false)
        {
            //TODO :: Refactor
            var item = new CacheItem(cacheItem.GetIdentifier(), cacheItem);
            CacheItemPolicy cacheItemPolicy = GetDefaultCacheItemPlocy();

            if (withSync)
            {
                _upperTierCacheClient.Insert(cacheItem,withSync);
            }

            _cacheInfraestructure.Set(item.Key, item, cacheItemPolicy);
            ItemAdded(cacheItem);
        }


        public T Get(string identifier, bool withSync = false)
        {
            if (withSync)
            {
                var syncItem = (T)_upperTierCacheClient.Get(identifier);
                Insert(syncItem);
            }

            var result = _cacheInfraestructure.Get(identifier);
            return result != null ?
                ((result as CacheItem).Value as T) :
                null;
        }

        public void Delete(string identifier, bool withSync = false)
        {
            if (withSync)
            {
                //_upperTierCacheClient.Delete(identifier);
            }

            _cacheInfraestructure.Remove(identifier);
        }

        private void ItemAdded(T item)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Persist(item);
        }

        private void ItemRemoved(CacheEntryRemovedArguments arguments)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Remove((T)(arguments.CacheItem.Value as CacheItem).Value);
        }

        private CacheItemPolicy GetDefaultCacheItemPlocy()
        {
            var cacheItemPolicy = new CacheItemPolicy() //TODO :: Modify expiration policy from app config
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1.0),
                RemovedCallback = ItemRemoved
            };
            return cacheItemPolicy;
        }
    }
}
