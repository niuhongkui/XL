﻿using System;
using XL.Framework.Utility;
using XL.Core.Cache;

namespace XL.Core.Service
{
    public abstract class ServiceFactory
    {
        public abstract T CreateService<T>() where T : class;
    }

    /// <summary>
    /// 直接引用提供服务
    /// </summary>
    public class RefServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
           //第一次通过反射创建服务实例，然后缓存住
            var interfaceName = typeof(T).Name;
            return CacheHelper.Get<T>(string.Format("Service_{0}", interfaceName), () =>
                {
                    return AssemblyHelper.FindTypeByInterface<T>();
                });
        }
    }

    
}
