using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.IocManager;
using JCE.Logs.Formats;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注册NLog日志操作
        /// </summary>
        /// <param name="builder">IOC生成器</param>
        /// <returns></returns>
        public static IIocBuilder AddNLog(this IIocBuilder builder)
        {            
            builder.RegisterServices(x => x.Register<ILogProviderFactory, JCE.Logs.NLog.LogProviderFactory>(Lifetime.LifetimeScope));
            builder.RegisterServices(x => x.Register<ILogFormat, ContentFormat>(Lifetime.LifetimeScope));
            builder.RegisterServices(x => x.Register<ILogContext, JCE.Utils.Logs.Core.LogContext>(Lifetime.LifetimeScope));
            builder.RegisterServices(x => x.Register<ILog, JCE.Logs.Log>(Lifetime.LifetimeScope));
            return builder;
        }
    }
}
