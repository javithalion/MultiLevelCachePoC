using MultiLevelCachePoC.CacheCore.EntityContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.CachCore.Tests.Support
{
    public class DummyObject : CacheableEntity
    {
        public Guid Id { get; set; }

        public string Information { get; set; }

        public override string GetUniqueHash()
        {
            return Id.ToString();
        }

        public static DummyObject GetAnInstance()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var info = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return new DummyObject()
            {
                Id = Guid.NewGuid(),
                Information = info
            };
        }
    }
}
