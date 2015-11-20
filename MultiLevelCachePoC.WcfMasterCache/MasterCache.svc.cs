using System;
using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.EntityContracts;

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

        public void Delete(string identifier, SyncMode syncMode = SyncMode.NoSync)
        {
            _masterCache.Delete(identifier, syncMode);
        }

        public long ElementsCount()
        {
            return _masterCache.ElementsCount();
        }

        public CacheableEntity Get(string identifier, SyncMode syncMode = SyncMode.NoSync)
        {
            return _masterCache.Get(identifier, syncMode);
        }

        public void Set(CacheableEntity item, SyncMode syncMode = SyncMode.NoSync)
        {
            _masterCache.Set(item, syncMode);
        }
    }
}
