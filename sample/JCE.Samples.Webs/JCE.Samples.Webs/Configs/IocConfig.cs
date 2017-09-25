﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Extras.IocManager;
using JCE.Core.DependencyInjection;
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
        }

        /// <summary>
        /// 加载基础设施
        /// </summary>
        /// <param name="iocBuilder"></param>
        private void LoadInfrastructure(IIocBuilder iocBuilder)
        {
            iocBuilder.RegisterServices(x => x.Register<IContext, WebContext>(Lifetime.Singleton));
            iocBuilder.RegisterServices(x => x.Register<IUserContext, NullUserContext>(Lifetime.LifetimeScope));
            iocBuilder.RegisterServices(x => x.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly()));
            iocBuilder.AddNLog();
        }
    }
}