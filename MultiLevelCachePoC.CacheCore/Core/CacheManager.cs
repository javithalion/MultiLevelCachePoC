using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using System;
using System.Runtime.Caching;

namespace MultiLevelCachePoC.CacheCore.Core
{
    public class CacheManager : ICacheManager
    {
        private readonly string _cacheName;
        private MemoryCache _cacheInfraestructure;
        private readonly IPersistenceEngine _persistenceEngine;
       

        public CacheManager(IPersistenceEngine engine = null)
        {
            _cacheName = "XXXX";
            _cacheInfraestructure = new MemoryCache(_cacheName);
            _persistenceEngine = engine;
            
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

        public void Insert(CacheableEntity cacheItem, bool withSync = false)
        {           
            var item = new CacheItem(cacheItem.GetUniqueHash(), cacheItem);            

            if (withSync)
            {
                //TODO
            }

            _cacheInfraestructure.Set(item.Key, item, GetDefaultCacheItemPlocy());
            ItemAdded(cacheItem);
        }


        public CacheableEntity Get(string identifier, bool withSync = false)
        {
            if (withSync)
            {
               //TODO
            }

            var result = _cacheInfraestructure.Get(identifier);
            return result != null ?
                (CacheableEntity)(result as CacheItem).Value :
                null;
        }

        public void Delete(string identifier, bool withSync = false)
        {
            if (withSync)
            {
                //TODO
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
