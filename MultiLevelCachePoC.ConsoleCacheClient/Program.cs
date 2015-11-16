using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;

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

            cache.Insert(engineA, true);

            var engineResult1 = cache.Get(engineB.GetIdentifier()); //should be null
            var engineResult2 = cache.Get(engineA.GetIdentifier()); //should return

            cache.Delete(engineA.GetIdentifier());

            var engineResult3 = cache.Get(engineA.GetIdentifier()); // should be null
            //var engineResult4 = cache.Get(engineA.GetIdentifier(),true); //should return
            //var engineResult5 = cache.Get(engineA.GetIdentifier()); //should return
        }
    }
}
