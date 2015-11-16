using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace MultiLevelCachePoC.CacheCore.Core
{
    public class CacheManager<T> : ILocalCache<T> where T : ICacheableEntity
    {
        private readonly string _cacheName;
        private MemoryCache _cacheInfraestructure;
        private readonly IPersistenceEngine<T> _persistenceEngine;

        public CacheManager(IPersistenceEngine<T> engine = null)
        {
            _cacheName = typeof(T).Name;
            _cacheInfraestructure = new MemoryCache(_cacheName);
            _persistenceEngine = engine;

            LoadInitialCacheContent();
        }

        private void LoadInitialCacheContent()
        {
            if (_persistenceEngine == null) return;

            foreach (var item in _persistenceEngine.Load())
                Insert((T)item);
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

            if (withSync)
            {
                //TODO :: Sinc with cache upper level
            }

            var item = new CacheItem(cacheItem.GetIdentifier(), cacheItem);
            var cacheItemPolicy = new CacheItemPolicy() //TODO :: Modify expiration policy from app config
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1.0),
                RemovedCallback = ItemRemoved
            };

            _cacheInfraestructure.Set(item.Key, item, cacheItemPolicy);
            ItemAdded(cacheItem);
        }

        private void ItemAdded(T item)
        {
            _persistenceEngine.Persist(item);
        }

        private void ItemRemoved(CacheEntryRemovedArguments arguments)
        {
            _persistenceEngine.Remove((T)(arguments.CacheItem.Value as CacheItem).Value);
        }

        public T Get(string identifier, bool withSync = false)
        {
            if (withSync)
            {
                //TODO :: Sinc with cache upper level and update current level
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
                //TODO :: Sinc with cache upper level
            }

            _cacheInfraestructure.Remove(identifier);
        }
    }
}
