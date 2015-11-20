using Moq;
using MultiLevelCachePoC.CachCore.Tests.Support;
using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.EntityContracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.CachCore.Tests.Core
{
    [TestFixture]
    public class MasterSlaveTestFixture
    {
        //Set without sync. 2 levels of cache
        [Test]
        public void SetWithoutSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            cacheManager.Set(dummyValue, SyncMode.NoSync);

            //Assert
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }

        //Get without sync 2 levels of cache
        [Test]
        public void GetWithoutSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);

            //Act            
            cacheManager.Get(string.Empty, SyncMode.NoSync);

            //Assert
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }

        //Delete without sync 2 levels of cache
        [Test]
        public void DeleteWithoutSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);

            //Act            
            cacheManager.Delete(string.Empty, SyncMode.NoSync);

            //Assert
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }


        //Set with sync. 2 levels of cache
        [Test]
        public void SetWithSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            cacheManager.Set(dummyValue, SyncMode.Sync);

            //Assert
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Once);
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);            
        }

        //Get with sync 2 levels of cache
        [Test]
        public void GetWithSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);

            //Act            
            cacheManager.Get(string.Empty, SyncMode.Sync);

            //Assert
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Once);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);            
        }

        //Delete with sync 2 levels of cache
        [Test]
        public void DeleteWithSyncOn2LevelsCache_Ok()
        {
            //Arrange
            var level2CacheMock = new Mock<ICacheManager>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", masterCache: level2CacheMock.Object);

            //Act            
            cacheManager.Delete(string.Empty, SyncMode.Sync);

            //Assert
            level2CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level2CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Once);
            level2CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level2CacheMock.Verify(x => x.ElementsCount(), Times.Never);            
        }


        //Set with parent sync. 3 levels of cache
        [Test]
        public void SetWithParentSyncOn3LevelsCache_Ok()
        {
            //Arrange
            var level3CacheMock = new Mock<ICacheManager>();
            ICacheManager level2Cache = new CacheManager("CacheForTestLevel2", masterCache: level3CacheMock.Object);
            ICacheManager level1Cache = new CacheManager("CacheForTestLevel2", masterCache: level2Cache);
            var dummyValue = DummyObject.GetAnInstance();

            //Act            
            level1Cache.Set(dummyValue, SyncMode.SyncParent);

            //Assert
            Assert.IsTrue(level2Cache.ElementsCount() == 1, "Parent sync should add the value to level 2 cache");

            level3CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level3CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }

        //Get with parent sync 3 levels of cache
        [Test]
        public void GetWithParentSyncOn3LevelsCache_Ok()
        {
            //Arrange
            var level3CacheMock = new Mock<ICacheManager>();
            ICacheManager level2Cache = new CacheManager("CacheForTestLevel2", masterCache: level3CacheMock.Object);
            ICacheManager level1Cache = new CacheManager("CacheForTestLevel2", masterCache: level2Cache);
            var dummyValue = DummyObject.GetAnInstance();
            level2Cache.Set(dummyValue);

            //Act            
            var result = level1Cache.Get(dummyValue.GetUniqueHash(), SyncMode.SyncParent);
            

            //Assert
            Assert.IsTrue(level2Cache.ElementsCount() == 1, "Parent sync should should keep value unchanged");
            Assert.IsNotNull(result, "REsult should not be null");
            Assert.IsTrue(dummyValue.Equals(dummyValue), "Retrieved value should be the same one we set on lvl 2 cache");

            level3CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level3CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }

        //Delete with parent sync 3 levels of cache
        [Test]
        public void DeleteWithParentSyncOn3LevelsCache_Ok()
        {
            //Arrange
            var level3CacheMock = new Mock<ICacheManager>();
            ICacheManager level2Cache = new CacheManager("CacheForTestLevel2", masterCache: level3CacheMock.Object);
            ICacheManager level1Cache = new CacheManager("CacheForTestLevel2", masterCache: level2Cache);
            var dummyValue = DummyObject.GetAnInstance();
            level1Cache.Set(dummyValue);
            level2Cache.Set(dummyValue);

            //Act            
            level1Cache.Delete(dummyValue.GetUniqueHash(), SyncMode.SyncParent);

            //Assert
            Assert.IsTrue(level2Cache.ElementsCount() == 0, "Parent sync should delete the value from level 2 cache");
            Assert.IsTrue(level1Cache.ElementsCount() == 0, "Parent sync should delte the value from level 1 cache");

            level3CacheMock.Verify(x => x.Set(It.IsAny<CacheableEntity>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<SyncMode>()), Times.Never);
            level3CacheMock.Verify(x => x.ClearCache(), Times.Never);
            level3CacheMock.Verify(x => x.ElementsCount(), Times.Never);
        }

    }
}
