using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JCE.Core.DependencyInjection;
using JCE.Logs.Formats;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册NLog日志操作
        /// </summary>
        /// <param name="builder">容器生成器</param>
        /// <returns></returns>
        public static void AddNLog(this ContainerBuilder builder)
        {
            builder.AddScoped<ILogProviderFactory, JCE.Logs.NLog.LogProviderFactory>();
            builder.AddScoped<ILogFormat, ContentFormat>();
            builder.AddScoped<ILogContext, JCE.Utils.Logs.Core.LogContext>();
            builder.AddScoped<ILog, JCE.Logs.Log>();
        }
    }
}
