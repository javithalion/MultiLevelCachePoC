namespace MultiLevelCachePoC.CacheContracts.EntityContracts
{    
    public class Engine : CacheableEntity
    {        
        public int Id { get; set; }
       
        public string Description { get; set; }
       
        public override string GetIdentifier()
        {
            return Id.ToString();
        }
    }
}
