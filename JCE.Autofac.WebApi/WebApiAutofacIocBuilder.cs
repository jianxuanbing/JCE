/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.WebApi
 * 文件名：WebApiAutofacIocBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：b8aa536a-ee16-4b1e-b02e-a5b85d5642a1
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 17:25:10
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 17:25:10
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using JCE.Autofac.Extensions;
using JCE.Core.Dependency;

namespace JCE.Autofac.WebApi
{
    /// <summary>
    /// WebApi-Autofac依赖注入初始化
    /// </summary>
    public class WebApiAutofacIocBuilder:IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="WebApiAutofacIocBuilder"/>类型的实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public WebApiAutofacIocBuilder(IServiceCollection services) : base(services)
        {
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddScoped<IIocResolver, WebApiIocResolver>();
        }

        /// <summary>
        /// 构建服务并设置WebApi平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterWebApiModelBinderProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            IDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            return (IServiceProvider)resolver.GetService(typeof(IServiceProvider));
        }
    }
}
