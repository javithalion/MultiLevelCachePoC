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
            var station_A = new Station() { Id = 1, Description = "This is the station A" };

            cacheManager.Insert(engine_A, true);
            cacheManager.Insert(station_A);
            var getResult_1 = cacheManager.Get(engine_A.GetUniqueHash());
            var getResult_2 = cacheManager.Get(station_A.GetUniqueHash());

            cacheManager.Delete(engine_A.GetUniqueHash());
            var getResult_3 = cacheManager.Get(engine_A.GetUniqueHash());
            var getResult_4 = cacheManager.Get(station_A.GetUniqueHash());

            var getResult_5 = cacheManager.Get(engine_A.GetUniqueHash(), true);
            var getResult_6 = cacheManager.Get(engine_A.GetUniqueHash());

        }
    }
}
