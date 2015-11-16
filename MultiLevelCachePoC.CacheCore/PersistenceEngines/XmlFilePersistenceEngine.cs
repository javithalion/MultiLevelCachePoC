using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MultiLevelCachePoC.CacheCore.PersistenceEngines
{
    public class XmlFilePersistenceEngine<T> : IPersistenceEngine<T> where T : CacheableEntity
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

        public IEnumerable<T> Load()
        {
            //TODO :: Refactor
            IList<T> result = new List<T>();
            var directory = new DirectoryInfo(_targetFolder);

            if (directory.GetFiles().Length > 0)
            {
                foreach (var file in directory.GetFiles())
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    using (TextReader reader = new StreamReader(file.FullName))
                    {
                        result.Add((T)deserializer.Deserialize(reader));
                    }
                }
            }
            return result;
        }

        public void Remove(T value)
        {
            string targetFile = Path.Combine(_targetFolder, value.GetIdentifier() + ".cache");
            File.Delete(targetFile);
        }

        public void Persist(T value)
        {
            string targetFile = Path.Combine(_targetFolder,value.GetIdentifier() + ".cache");

            XmlSerializer serializer = new XmlSerializer(value.GetType());
            using (TextWriter writer = new StreamWriter(targetFile))
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
