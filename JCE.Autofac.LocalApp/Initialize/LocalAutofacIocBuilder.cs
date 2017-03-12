/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.LocalApp.Initialize
 * 文件名：LocalAutofacIocBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：ac076be1-5b49-46d7-b11b-593c3c8bec6b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 13:50:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 13:50:50
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
using Autofac;
using JCE.Autofac.Extensions;
using JCE.Core.Dependency;

namespace JCE.Autofac.LocalApp.Initialize
{
    /// <summary>
    /// 本地程序-Autofac依赖注入初始化
    /// </summary>
    public class LocalAutofacIocBuilder: IocBuilderBase
    {
        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        public IIocResolver Resolver { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="LocalAutofacIocBuilder"/>类型的实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public LocalAutofacIocBuilder(IServiceCollection services) : base(services)
        {
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            services.AddSingleton<IIocResolver, LocalIocResolver>();
            
        }

        /// <summary>
        /// 构建服务并设置本地程序平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder=new ContainerBuilder();
            builder.Populate(services);
            IContainer container = builder.Build();
            LocalIocResolver.Container = container;
            Resolver = container.Resolve<IIocResolver>();
            return Resolver.Resolve<IServiceProvider>();
        }
    }
}
