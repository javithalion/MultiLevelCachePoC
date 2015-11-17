using MultiLevelCachePoC.CacheContracts.EntityContracts;
using MultiLevelCachePoC.CacheCore.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml;
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
            //TODO :: Refactor!!
            IList<CacheableEntity> result = new List<CacheableEntity>();
            var directory = new DirectoryInfo(_targetFolder);

            if (directory.GetFiles().Length > 0)
            {
                var declaredTypes = ReflectionHelper.GetCAcheableEntityTypeDescendants();
                foreach (var file in directory.GetFiles())
                {
                    XmlSerializer deserializer;
                    foreach (var type in declaredTypes)
                    {
                        deserializer = new XmlSerializer(type);
                        using (XmlReader reader = new XmlTextReader(file.FullName))
                        {
                            if (deserializer.CanDeserialize(reader))
                            {
                                result.Add((CacheableEntity)deserializer.Deserialize(reader));
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void Remove(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetUniqueHash() + ".cache");
            File.Delete(targetFile);
        }

        public void Persist(CacheableEntity value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetUniqueHash() + ".cache");

            XmlSerializer serializer = new XmlSerializer(value.GetType());
            using (TextWriter writer = new StreamWriter(targetFile))
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
