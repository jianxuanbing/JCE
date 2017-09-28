using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Autofac;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// AspectCore扩展
    /// </summary>
    public static class AopExtensions
    {
        /// <summary>
        /// 启用AOP作用域
        /// </summary>
        public static void EnableAspectScoped(this ContainerBuilder builder)
        {
            builder.AddSingleton<IAspectActivatorFactory, AspectActivatorFactory>();
            builder.AddSingleton<IAspectBuilderFactory, AspectBuilderFactory>();
            builder.AddScoped<IAspectContextFactory, AspectContextFactory>();
        }
    }
}
