using MultiLevelCachePoC.CacheCore.EntityContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MultiLevelCachePoC.CacheCore.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetCacheableEntityTypeDescendants()
        {           
            List<Type> result = new List<Type>();

            var mainType = typeof(CacheableEntity);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var subTypes = assembly.GetTypes().Where(x => x.IsSubclassOf(mainType)).ToArray();
                result.AddRange(subTypes.Where(x => subTypes.All(y => y.BaseType != x)));
            }

            return result;
        }
    }
}
