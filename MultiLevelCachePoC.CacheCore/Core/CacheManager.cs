using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using MultiLevelCachePoC.CacheCore.UpperLevelCache;
using System;
using System.Configuration;
using System.Runtime.Caching;


namespace MultiLevelCachePoC.CacheCore.Core
{
    public class CacheManager : CacheContracts.ApiContracts.ICacheManager
    {
        private readonly string _cacheName;
        private readonly IPersistenceEngine _persistenceEngine;
        private MemoryCache _cacheInfraestructure;
        private readonly UpperLevelCache.ICacheManager _upperLevelCacheClient;
        private readonly bool _isCacheSlave;        

        public CacheManager(string name, IPersistenceEngine engine = null)
        {
            _cacheName = name;
            _cacheInfraestructure = new MemoryCache(_cacheName);
            _isCacheSlave = ConfigurationManager.AppSettings["IsCacheSlave"].ToString().ToLower().StartsWith("y");
            _persistenceEngine = engine;

            if (_isCacheSlave)
                _upperLevelCacheClient = new CacheManagerClient();           

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

        public void Insert(CacheableEntity cacheItem, SyncMode syncMode = SyncMode.WithoutSync)
        {
            var item = new CacheItem(cacheItem.GetUniqueHash(), cacheItem);

            if (_isCacheSlave && syncMode == SyncMode.WithSync )
            {
                _upperLevelCacheClient.Insert(cacheItem, false);
            }

            _cacheInfraestructure.Set(item.Key, item, GetDefaultCacheItemPlocy());
            ItemAdded(cacheItem);
        }


        public CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.WithoutSync)
        {
            if (_isCacheSlave && syncMode == SyncMode.WithSync)
            {
                var syncObject = _upperLevelCacheClient.Get(identifier, false);
                if (syncObject != null)
                    Insert(syncObject, SyncMode.WithoutSync);
            }

            var result = _cacheInfraestructure.Get(identifier);
            return result != null ?
                (CacheableEntity)(result as CacheItem).Value :
                null;
        }

        public void Delete(string identifier, SyncMode syncMode = SyncMode.WithoutSync)
        {
            if (_isCacheSlave && syncMode == SyncMode.WithSync)
            {
                _upperLevelCacheClient.Delete(identifier, false);
            }

            _cacheInfraestructure.Remove(identifier);
        }

        private void ItemAdded(CacheableEntity item)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Persist(item);
        }

        private void ItemRemoved(CacheEntryRemovedArguments arguments)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Remove((CacheableEntity)(arguments.CacheItem.Value as CacheItem).Value);
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
