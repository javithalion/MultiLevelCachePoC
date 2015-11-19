using MultiLevelCachePoC.CacheCore.EntityContracts;

namespace MultiLevelCachePoC.Domain
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
