using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.ConsoleLocalCache
{
    public class Station : CacheableEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public override string GetUniqueHash()
        {
            return string.Format("Station_{0}", Id);
        }
    }
}
