using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public class XmlFilePersistenceEngine : IPersistenceEngine
    {
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
            //TODO :: Refactor
            IList<CacheableEntity> result = new List<CacheableEntity>();
            var directory = new DirectoryInfo(_targetFolder);

            if (directory.GetFiles().Length > 0)
            {
                foreach (var file in directory.GetFiles())
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(CacheableEntity));
                    using (TextReader reader = new StreamReader(file.FullName))
                    {
                        result.Add((CacheableEntity)deserializer.Deserialize(reader));
                    }
                }
            }
            return result;
        }

        public void Remove(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetHashCode() + ".cache");
            File.Delete(targetFile);
        }

        public void Persist(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder,value.GetHashCode() + ".cache");

            XmlSerializer serializer = new XmlSerializer(value.GetType());
            using (TextWriter writer = new StreamWriter(targetFile))
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
