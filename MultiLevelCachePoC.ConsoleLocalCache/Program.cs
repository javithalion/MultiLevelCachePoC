using MultiLevelCachePoC.CacheContracts.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.ConsoleLocalCache
{
    class Program
    {
        static void Main(string[] args)
        {
            IPersistenceEngine persistenceEngine = new XmlFilePersistenceEngine(@".\CacheContent\");
            ICacheManager cacheManager = new CacheManager(persistenceEngine);


            var engine_A = new Engine() { Id = 1, Description = "This is the engine A" };

            cacheManager.Insert(engine_A);
            var getResult1 = cacheManager.Get(engine_A.GetUniqueHash());

            cacheManager.Delete(engine_A.GetUniqueHash());
            var getResult2 = cacheManager.Get(engine_A.GetUniqueHash());

        }
    }
}
