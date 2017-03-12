/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.Mvc
 * 文件名：MvcAutofacIocBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：87939871-8a4d-46c2-8563-d3dc25501a37
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 16:24:21
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 16:24:21
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
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JCE.Autofac.Extensions;
using JCE.Core.Dependency;

namespace JCE.Autofac.Mvc
{
    /// <summary>
    /// Mvc-Autofac依赖注入初始化
    /// </summary>
    public class MvcAutofacIocBuilder:IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="MvcAutofacIocBuilder"/>类型的实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public MvcAutofacIocBuilder(IServiceCollection services) : base(services)
        {
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, MvcIocResolver>();
        }

        /// <summary>
        /// 构建服务并设置Mvc平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder=new ContainerBuilder();
            builder.RegisterControllers(assemblies).AsSelf().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.Populate(services);
            IContainer container = builder.Build();
            AutofacDependencyResolver resolver=new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            MvcIocResolver.GlobalResolveFunc = t => resolver.ApplicationContainer.Resolve(t);
            return resolver.GetService<IServiceProvider>();
        }
    }
}
