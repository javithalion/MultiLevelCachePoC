using Moq;
using MultiLevelCachePoC.CachCore.Tests.Support;
using MultiLevelCachePoC.CacheCore.ApiContracts;
using MultiLevelCachePoC.CacheCore.Core;
using MultiLevelCachePoC.CacheCore.EntityContracts;
using MultiLevelCachePoC.CacheCore.PersistenceEngines;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MultiLevelCachePoC.CachCore.Tests.Core
{
    [TestFixture]
    public class PersistencEngineTestFixture
    {

        [Test]
        public void InitialLoad_Ok()
        {
            //Arrange
            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);

            //Act            

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Never);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once);
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Never);
        }

        [Test]
        public void InitialLoadWithEntities_Ok()
        {
            //Arrange
            var listOfDummyObjects = new List<CacheableEntity>() { DummyObject.GetAnInstance(), DummyObject.GetAnInstance(), DummyObject.GetAnInstance() };

            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            persistenceEngineMock.Setup(f => f.Load()).Returns(listOfDummyObjects);

            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);

            //Act            

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Never);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once);
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Exactly(listOfDummyObjects.Count));

            Assert.IsTrue(
                listOfDummyObjects.All(o => cacheManager.Get(o.GetUniqueHash()) != null),
                "Loaded items are not present within the cache!");
            Assert.IsTrue(cacheManager.ElementsCount() == listOfDummyObjects.Count, "We should have just the elements we added during the initial load");
        }

        [Test]
        public void NewItemGetsPersisted_Ok()
        {
            //Arrange
            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);
            var dummyObject = DummyObject.GetAnInstance();

            //Act
            cacheManager.Set(dummyObject);

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Never);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once); //Initial call when load
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Once);
        }

        [Test]
        public void ExistingItemGetsRemoved_Ok()
        {
            //Arrange
            var dummyObject = DummyObject.GetAnInstance();

            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            persistenceEngineMock.Setup(f => f.Load()).Returns(new List<CacheableEntity>() { dummyObject });

            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);

            //Act
            cacheManager.Delete(dummyObject.GetUniqueHash());

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Once);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once); //Initial call when load
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Once); //The addition action from load
        }

        [Test]
        public void AddExistingItemGetsRemovedAndAddedAgain_Ok()
        {
            //Arrange
            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);
            var dummyObject = DummyObject.GetAnInstance();

            //Act
            cacheManager.Set(dummyObject);
            cacheManager.Set(dummyObject);

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Once);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once); //Initial call when load
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Exactly(2));
        }

        [Test]
        public void NonExistingItemIsNotRemoved_Ok()
        {
            //Arrange
            var dummyObject = DummyObject.GetAnInstance();

            var persistenceEngineMock = new Mock<IPersistenceEngine>();
            ICacheManager cacheManager = new CacheManager("CacheForTest", persistenceEngineMock.Object);

            //Act
            cacheManager.Delete(dummyObject.GetUniqueHash());

            //Assert
            persistenceEngineMock.Verify(x => x.Remove(It.IsAny<CacheableEntity>()), Times.Never);
            persistenceEngineMock.Verify(x => x.Load(), Times.Once); //Initial call when load
            persistenceEngineMock.Verify(x => x.Persist(It.IsAny<CacheableEntity>()), Times.Never);
        }
    }
}
