using MultiLevelCachePoC.CachCore.Tests.Support;
using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.CachCore.Tests.Core
{
    [TestFixture]
    public class BasicCacheTestFixture
    {
        [Test]
        public void BasicCacheCreation_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);

            //Act            

            //Assert
            Assert.IsTrue((cacheManager as CacheManager).CacheName == cacheName, "Name was not properly set");
            Assert.IsFalse((cacheManager as CacheManager).IsSlaveCache, "Cache was not marked as expected (should not be a slave cache)");
        }

        [Test]
        public void BasicCacheActionElementCount_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);

            //Act      
            var result = cacheManager.ElementsCount();

            //Assert
            Assert.IsTrue(result == 0, "On just created cache we should have 0 elements");
        }

        [Test]
        public void BasicCacheActionGetNonExisting_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);

            //Act      
            var result = cacheManager.Get("NonExisitingKey");

            //Assert
            Assert.IsNull(result, "Non existing key should return null value");
        }

        [Test]
        public void BasicCacheActionSet_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            cacheManager.Set(dummyValue);
            var totalElements = cacheManager.ElementsCount();

            //Assert
            Assert.IsTrue(totalElements == 1, "We should have just one element within the cache");

        }

        [Test]
        public void BasicCacheActionGetExisting_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);
            var dummyValue = DummyObject.GetAnInstance();

            //Act      
            cacheManager.Set(dummyValue);
            var result = cacheManager.Get(dummyValue.GetUniqueHash());

            //Assert
            Assert.IsNotNull(result, "Existing key should not return null value");
            Assert.IsTrue(dummyValue.Equals(result), "Objects are not the same");
        }

        [Test]
        public void BasicCacheActionDelete_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            cacheManager.Set(dummyValue);
            var totalElementsBeforeDelete = cacheManager.ElementsCount();

            cacheManager.Delete(dummyValue.GetUniqueHash());
            var totalElementsAfterDelete = cacheManager.ElementsCount();

            //Assert
            Assert.IsTrue(totalElementsBeforeDelete == 1, "We should have just one element within the cache");
            Assert.IsTrue(totalElementsAfterDelete == 0, "We should have zero elements within the cache");
        }

        [Test]
        public void BasicCacheActionClearCache_Ok()
        {
            //Arrange   
            string cacheName = "CacheForTest";
            ICacheManager cacheManager = new CacheManager(cacheName);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            cacheManager.Set(dummyValue);
            var totalElementsBeforeClear = cacheManager.ElementsCount();

            cacheManager.ClearCache();
            var totalElementsAfterClear = cacheManager.ElementsCount();

            //Assert
            Assert.IsTrue(totalElementsBeforeClear == 1, "We should have just one element within the cache");
            Assert.IsTrue(totalElementsAfterClear == 0, "We should have zero elements within the cache");
        }
    }
}
