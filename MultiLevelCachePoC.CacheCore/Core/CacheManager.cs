using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using System;
using System.Runtime.Caching;

namespace MultiLevelCachePoC.CacheCore.Core
{
    public class CacheManager : ICacheManager
    {
        private readonly string _cacheName;
        private readonly IPersistenceEngine _persistenceEngine;
        private MemoryCache _cacheInfraestructure;
        private readonly ICacheManager _masterCache;
        private readonly bool _isSlaveCache;

        private static readonly object _locker = new object();

        public CacheManager(string name, IPersistenceEngine engine = null, ICacheManager masterCache = null)
        {
            _cacheName = name;
            _cacheInfraestructure = new MemoryCache(_cacheName);
            _persistenceEngine = engine;
            _isSlaveCache = masterCache != null;
            _masterCache = masterCache;

            LoadInitialCacheContent();
        }

        private void LoadInitialCacheContent()
        {
            if (_persistenceEngine == null) return;

            foreach (var item in _persistenceEngine.Load())
                Set(item);
        }

        public void ClearCache()
        {
            lock (_locker)
            {
                _cacheInfraestructure.Dispose();
                _cacheInfraestructure = new MemoryCache(_cacheName);
            }
        }

        public void Set(CacheableEntity cacheItem, SyncMode syncMode = SyncMode.NoSync)
        {
            var item = new CacheItem(cacheItem.GetUniqueHash(), cacheItem);

            if (_isSlaveCache && syncMode != SyncMode.NoSync)
                _masterCache.Set(cacheItem, GetSyncModeForParentCache(syncMode));

            lock (_locker)
                _cacheInfraestructure.Set(item.Key, item, GetDefaultCacheItemPolicy());

            ItemEstablished(cacheItem);
        }

        public CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.NoSync)
        {
            if (_isSlaveCache && syncMode != SyncMode.NoSync)
            {
                var syncObject = _masterCache.Get(identifier, GetSyncModeForParentCache(syncMode));
                if (syncObject != null)
                    Set(syncObject, SyncMode.NoSync);
            }

            CacheItem result;
            lock (_locker)
                result = _cacheInfraestructure.Get(identifier) as CacheItem;

            return result != null ? result.Value as CacheableEntity : null;
        }

        public void Delete(string identifier, SyncMode syncMode = SyncMode.NoSync)
        {
            if (_isSlaveCache && syncMode != SyncMode.NoSync)
                _masterCache.Delete(identifier, GetSyncModeForParentCache(syncMode));

            lock (_locker)
                _cacheInfraestructure.Remove(identifier);
        }

        private void ItemEstablished(CacheableEntity item)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Persist(item);
        }

        private void ItemRemoved(CacheEntryRemovedArguments arguments)
        {
            if (_persistenceEngine != null)
                _persistenceEngine.Remove((CacheableEntity)(arguments.CacheItem.Value as CacheItem).Value);
        }

        private SyncMode GetSyncModeForParentCache(SyncMode syncMode)
        {
            return syncMode != SyncMode.Sync ?
                SyncMode.NoSync :
                SyncMode.Sync;
        }

        private CacheItemPolicy GetDefaultCacheItemPolicy()
        {
            return new CacheItemPolicy() //TODO :: Modify expiration policy from app config
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1.0),
                RemovedCallback = ItemRemoved
            };            
        }
    }
}
