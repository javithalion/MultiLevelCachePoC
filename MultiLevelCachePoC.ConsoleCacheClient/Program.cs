using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.ConsoleCacheClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var persistenceEngine = new XmlFilePersistenceEngine<Engine>(@".\CacheContent\");
            ILocalCache<Engine> cache = new CacheManager<Engine>(persistenceEngine);

            var engineA = new Engine()
            {
                Id = 1,
                Description = "This is the engine A"
            };

            var engineB = new Engine()
            {
                Id = 2,
                Description = "This is the engine B"
            };

            cache.Insert(engineA);

            var engineResult1 = cache.Get(engineB.GetIdentifier());
            var engineResult2 = cache.Get(engineA.GetIdentifier());

            cache.Delete(engineA.GetIdentifier());

            var engineResult3 = cache.Get(engineA.GetIdentifier());
        }
    }
}
