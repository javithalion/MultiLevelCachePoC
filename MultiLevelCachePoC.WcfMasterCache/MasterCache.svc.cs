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

        public void Delete(string identifier, SyncMode syncMode = SyncMode.WithoutSync)
        {
            _masterCache.Delete(identifier, syncMode);
        }

        public CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.WithoutSync)
        {
            return _masterCache.Get(identifier, syncMode);
        }

        public void Insert(CacheableEntity item, SyncMode syncMode = SyncMode.WithoutSync)
        {
            _masterCache.Insert(item, syncMode);
        }
    }
}
