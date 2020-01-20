using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using XL.Framework.BLL;
using XL.Framework.DAL;
using XL.Framework.Utility;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace XL.Framework.Web
{
    /// <summary>
    /// Autofac依赖注入配置
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialise()
        {
            var builder = RegisterService();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// 注入实现
        /// </summary>
        /// <returns></returns>
        private static ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();
            var baseType = typeof(IDependency);
            //扫描IService和Service相关的程序集
            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(m => m.FullName.Contains("Exam")).ToList();
            builder.RegisterControllers(assemblys.ToArray());
            builder.RegisterApiControllers(assemblys.ToArray());
            //自动注入
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
            return builder;
        }
    }
}
