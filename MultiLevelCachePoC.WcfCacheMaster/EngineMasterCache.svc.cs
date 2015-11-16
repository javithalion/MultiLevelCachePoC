using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiLevelCachePoC.WcfCacheMaster
{
    public class MasterCache : IMasterCache
    {
        private static readonly IDictionary<Type, ILocalCache<CacheableEntity>> _cacheCollection;

        static MasterCache()
        {
            localCache = new CacheManager<Engine>();
        }

        public void ClearCache()
        {
            Debug.WriteLine("ClearCache");
            GetCacheForType().ClearCache();
        }

        public void Delete(string identifier, bool withSync = false)
        {
            Debug.WriteLine("ClearCache");
            _localCache.Delete(identifier, withSync);
        }

        public CacheableEntity Get(string identifier, bool withSync = false)
        {
            Debug.WriteLine("ClearCache");
            return _localCache.Get(identifier, withSync);
        }

        public void Insert(CacheableEntity cacheItem, bool withSync = false)
        {
            Debug.WriteLine("ClearCache");
            _localCache.Insert(cacheItem, withSync);
        }

        private ILocalCache<CacheableEntity> GetCacheForType(Type type)
        {
            if (_cacheCollection.ContainsKey(type))
                _cacheCollection.Add(type, new CacheManager<>();

            return _cacheCollection[type];
        }

        
    }
}
