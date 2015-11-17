using MultiLevelCachePoC.CacheContracts.ApiContracts;
using System;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.Core;

namespace MultiLevelCachePoC.WcfMasterCache
{
    public class MasterCache : ICacheManager
    {
        private static readonly ICacheManager _masterCache;

        static MasterCache()
        {
            _masterCache = new CacheManager("MasterCache");
        }

        public void ClearCache()
        {
            _masterCache.ClearCache();
        }

        public void Delete(string identifier, bool withSync = false)
        {
            _masterCache.Delete(identifier, withSync);
        }

        public CacheableEntity Get(string identifier, bool withSync = false)
        {
            return _masterCache.Get(identifier, withSync);
        }

        public void Insert(CacheableEntity item, bool withSync = false)
        {
            _masterCache.Insert(item, withSync);
        }
    }
}
