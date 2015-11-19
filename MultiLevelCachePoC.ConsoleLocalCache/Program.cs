using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using MultiLevelCachePoC.Domain;
using System.ServiceModel;

namespace MultiLevelCachePoC.ConsoleLocalCache
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ICacheManager> channelFactory = new ChannelFactory<ICacheManager>("*");
            ICacheManager masterCache = channelFactory.CreateChannel();

            IPersistenceEngine persistenceEngine = new XmlFilePersistenceEngine(@".\CacheContent\");

            ICacheManager cacheManager = new CacheManager("LocalCache", persistenceEngine, masterCache);

            var engine_A = new Engine() { Id = 1, Description = "This is the engine A" };
            var station_A = new Station() { Id = 1, Description = "This is the station A" };

            cacheManager.Set(engine_A, SyncMode.Sync);
            cacheManager.Set(station_A);

            var getResult_1 = cacheManager.Get(engine_A.GetUniqueHash());
            var getResult_2 = cacheManager.Get(station_A.GetUniqueHash());

            cacheManager.Delete(engine_A.GetUniqueHash());
            var getResult_3 = cacheManager.Get(engine_A.GetUniqueHash());
            var getResult_4 = cacheManager.Get(station_A.GetUniqueHash());

            var getResult_5 = cacheManager.Get(engine_A.GetUniqueHash(), SyncMode.Sync);
            var getResult_6 = cacheManager.Get(engine_A.GetUniqueHash());

        }
    }
}
