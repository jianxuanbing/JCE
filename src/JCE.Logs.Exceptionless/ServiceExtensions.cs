using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Exceptionless;
using JCE.Core.DependencyInjection;
using JCE.Logs.Formats;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Core;

namespace JCE.Logs.Exceptionless
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册 Exceptionless 日志操作
        /// </summary>
        /// <param name="builder">容器生成器</param>
        /// <param name="configAction">配置操作</param>
        public static void AddExceptionless(this ContainerBuilder builder,Action<ExceptionlessConfiguration> configAction)
        {
            ExceptionlessConfig.Register();
            builder.AddScoped<ILogProviderFactory, JCE.Logs.Exceptionless.LogProviderFactory>();
            builder.AddScoped<ILogFormat, NullLogFormat>();
            builder.AddScoped<ILogContext, JCE.Logs.Exceptionless.LogContext>();
            builder.AddScoped<ILog, JCE.Logs.Log>();
            configAction?.Invoke(ExceptionlessClient.Default.Configuration);
        }
    }
}
