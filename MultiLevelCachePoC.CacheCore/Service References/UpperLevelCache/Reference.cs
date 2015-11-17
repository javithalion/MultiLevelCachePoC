﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiLevelCachePoC.CacheCore.UpperLevelCache {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UpperLevelCache.ICacheManager")]
    public interface ICacheManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Insert", ReplyAction="http://tempuri.org/ICacheManager/InsertResponse")]
        void Insert(MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity item, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Insert", ReplyAction="http://tempuri.org/ICacheManager/InsertResponse")]
        System.Threading.Tasks.Task InsertAsync(MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity item, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Get", ReplyAction="http://tempuri.org/ICacheManager/GetResponse")]
        MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity Get(string identifier, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Get", ReplyAction="http://tempuri.org/ICacheManager/GetResponse")]
        System.Threading.Tasks.Task<MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity> GetAsync(string identifier, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Delete", ReplyAction="http://tempuri.org/ICacheManager/DeleteResponse")]
        void Delete(string identifier, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/Delete", ReplyAction="http://tempuri.org/ICacheManager/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(string identifier, bool withSync);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/ClearCache", ReplyAction="http://tempuri.org/ICacheManager/ClearCacheResponse")]
        void ClearCache();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICacheManager/ClearCache", ReplyAction="http://tempuri.org/ICacheManager/ClearCacheResponse")]
        System.Threading.Tasks.Task ClearCacheAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICacheManagerChannel : MultiLevelCachePoC.CacheCore.UpperLevelCache.ICacheManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CacheManagerClient : System.ServiceModel.ClientBase<MultiLevelCachePoC.CacheCore.UpperLevelCache.ICacheManager>, MultiLevelCachePoC.CacheCore.UpperLevelCache.ICacheManager {
        
        public CacheManagerClient() {
        }
        
        public CacheManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CacheManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CacheManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CacheManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Insert(MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity item, bool withSync) {
            base.Channel.Insert(item, withSync);
        }
        
        public System.Threading.Tasks.Task InsertAsync(MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity item, bool withSync) {
            return base.Channel.InsertAsync(item, withSync);
        }
        
        public MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity Get(string identifier, bool withSync) {
            return base.Channel.Get(identifier, withSync);
        }
        
        public System.Threading.Tasks.Task<MultiLevelCachePoC.CacheContracts.EntityContracts.CacheableEntity> GetAsync(string identifier, bool withSync) {
            return base.Channel.GetAsync(identifier, withSync);
        }
        
        public void Delete(string identifier, bool withSync) {
            base.Channel.Delete(identifier, withSync);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(string identifier, bool withSync) {
            return base.Channel.DeleteAsync(identifier, withSync);
        }
        
        public void ClearCache() {
            base.Channel.ClearCache();
        }
        
        public System.Threading.Tasks.Task ClearCacheAsync() {
            return base.Channel.ClearCacheAsync();
        }
    }
}