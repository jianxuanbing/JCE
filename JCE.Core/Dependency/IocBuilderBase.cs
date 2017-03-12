/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IocBuilderBase
 * 版本号：v1.0.0.0
 * 唯一标识：2c8c164a-8ecb-4f01-a7ae-9201fd67c3fd
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：9/7 星期三 17:36:26
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：9/7 星期三 17:36:26
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
using JCE.Core.Reflection;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入构建器基类，从程序集中反射进行依赖注入接口与实现的注册
    /// </summary>
    public abstract class IocBuilderBase:IIocBuilder
    {
        /// <summary>
        /// 服务映射集合
        /// </summary>
        private readonly IServiceCollection _services;
        /// <summary>
        /// 是否已构建
        /// </summary>
        private bool _isBuilded;

        /// <summary>
        /// 初始化一个<see cref="IocBuilderBase"/>类型的实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected IocBuilderBase(IServiceCollection services)
        {
            AssemblyFinder=new DirectoryAssemblyFinder();
            _services = services.Clone();
            _isBuilded = false;
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }
        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }
        
        /// <summary>
        /// 开始构建依赖注入映射
        /// </summary>
        /// <returns>服务提供者</returns>
        public IServiceProvider Build()
        {
            if (_isBuilded)
            {
                return ServiceProvider;
            }
            //设置各个框架的DependencyResolver
            Assembly[] assemblies = AssemblyFinder.FindAll();
            AddCustomTypes(_services);

            ServiceProvider = BuildAndSetResolver(_services, assemblies);
            _isBuilded = true;
            return ServiceProvider;
        }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected abstract void AddCustomTypes(IServiceCollection services);        
        /// <summary>
        /// 重写以实现构建服务并设置各个平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务信息集合</returns>
        protected abstract IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies);
    }
}
