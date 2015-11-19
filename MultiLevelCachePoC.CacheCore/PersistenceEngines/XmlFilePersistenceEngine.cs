using MultiLevelCachePoC.CacheCore.EntityContracts;
using MultiLevelCachePoC.CacheCore.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public class XmlFilePersistenceEngine : IPersistenceEngine
    {
        private const string CacheFileExtension = ".cache";
        private readonly string _targetFolder;


        public XmlFilePersistenceEngine(string targetFolder)
        {
            CheckFolder(targetFolder);
            _targetFolder = targetFolder;
        }

        private void CheckFolder(string targetFolder)
        {
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);
        }

        public IEnumerable<CacheableEntity> Load()
        {
            IList<CacheableEntity> result = new List<CacheableEntity>();

            var declaredTypes = ReflectionHelper.GetCacheableEntityTypeDescendants();
            foreach (var file in GetCacheFilesFromTargetDirectory())
            {
                foreach (var type in declaredTypes)
                {
                    var cacheableEntityDeserialized = DeserializeFileToType(file.FullName, type);
                    if (cacheableEntityDeserialized != null)
                    {
                        result.Add(cacheableEntityDeserialized);
                        break;
                    }
                }
            }

            return result;
        }

        private CacheableEntity DeserializeFileToType(string fileFullName, Type type)
        {
            XmlSerializer deserializer = new XmlSerializer(type);
            using (XmlReader reader = new XmlTextReader(fileFullName))
            {
                return deserializer.CanDeserialize(reader) ?
                (CacheableEntity)deserializer.Deserialize(reader) :
                null;
            }
        }

        private IEnumerable<FileInfo> GetCacheFilesFromTargetDirectory()
        {
            var directory = new DirectoryInfo(_targetFolder);
            return directory.GetFiles().Where(f => f.Extension.ToLower() == CacheFileExtension.ToLower());
        }

        public void Remove(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetUniqueHash() + CacheFileExtension);
            File.Delete(targetFile);
        }

        public void Persist(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetUniqueHash() + CacheFileExtension);

            XmlSerializer serializer = new XmlSerializer(value.GetType());
            using (TextWriter writer = new StreamWriter(targetFile))
            {
                serializer.Serialize(writer, value);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
