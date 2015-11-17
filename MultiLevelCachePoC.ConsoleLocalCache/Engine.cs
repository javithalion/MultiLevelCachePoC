using MultiLevelCachePoC.CacheContracts.EntityContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCachePoC.ConsoleLocalCache
{
    public class Engine : CacheableEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public override string GetUniqueHash()
        {
            return Id.ToString();
        }
    }
}
