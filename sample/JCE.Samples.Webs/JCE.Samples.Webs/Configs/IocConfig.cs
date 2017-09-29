using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Extras.IocManager;
using JCE.Core.DependencyInjection;
using JCE.Logs.Exceptionless;
using JCE.Logs.Log4Net;
using JCE.Logs.NLog;
using JCE.Utils.Contexts;

namespace JCE.Samples.Webs.Configs
{
    /// <summary>
    /// IOC配置
    /// </summary>
    public class IocConfig:ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            LoadInfrastructure(builder);
        }

        /// <summary>
        /// 加载基础设施
        /// </summary>
        /// <param name="builder"></param>
        private void LoadInfrastructure(ContainerBuilder builder)
        {
            builder.AddScoped<IContext, WebContext>();
            builder.AddScoped<IUserContext, NullUserContext>();
            //builder.AddNLog();
            //builder.AddLog4Net();
            builder.AddExceptionless(config =>
            {
                config.ApiKey = "CqcBoQlNP1FBxCWLe0o5ZpX3eSmB3JqK4QUvDGUw";
                config.ServerUrl = "http://192.168.88.20:10240";
            });
        }
    }
}