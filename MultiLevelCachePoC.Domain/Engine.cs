using MultiLevelCachePoC.CacheContracts.EntityContracts;

namespace MultiLevelCachePoC.Domain
{
    public class Engine : CacheableEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public override string GetUniqueHash()
        {
            return string.Format("Engine_{0}", Id);
        }
    }
}
