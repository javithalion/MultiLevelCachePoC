using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.CacheContracts.ApiContracts
{
    public interface ISharedCacheService : ILocalCache<ICacheableEntity>
    {

    }
}
