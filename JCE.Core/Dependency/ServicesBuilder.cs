/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServicesBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：3ed32b80-f21b-42c8-a67f-eefdbbbfa013
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 16:04:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 16:04:00
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
    /// 服务器映射集合创建功能
    /// </summary>
    public class ServicesBuilder:IServicesBuilder
    {
        private readonly ServiceBuildOptions _options;

        #region 构造函数
        /// <summary>
        /// 初始化一个<see cref="ServicesBuilder"/>类型的新实例
        /// </summary>
        public ServicesBuilder() : this(new ServiceBuildOptions())
        {
            
        }
        /// <summary>
        /// 初始化一个<see cref="ServicesBuilder"/>类型的新实例
        /// </summary>
        /// <param name="options">服务创建配置信息</param>
        public ServicesBuilder(ServiceBuildOptions options)
        {
            _options = options;
        }
        #endregion
        
        /// <summary>
        /// 构建当前服务，并添加到服务映射集合中
        /// </summary>
        /// <returns>服务映射集合</returns>
        public IServiceCollection Build()
        {
            IServiceCollection services=new ServiceCollection();
            ServiceBuildOptions options = _options;
            try
            {
                //添加即时生命周期类型的映射
                Type[] dependencyTypes = options.TransientTypeFinder.FindAll();
                AddTypeWithInterfaces(services,dependencyTypes,LifetimeStyle.Transient);

                //添加局部生命周期类型的映射
                dependencyTypes = options.ScopeTypeFinder.FindAll();
                AddTypeWithInterfaces(services, dependencyTypes, LifetimeStyle.Scoped);

                //添加单例生命周期类型的映射
                dependencyTypes = options.SingletonTypeFinder.FindAll();
                AddTypeWithInterfaces(services, dependencyTypes, LifetimeStyle.Singleton);

                //全局服务
                AddGlobalTypes(services);
            }
            catch (ReflectionTypeLoadException ex)
            {
                Exception[] loadExs = ex.LoaderExceptions;
                throw;
            }
            return services;
        }
        /// <summary>
        /// 以类型实现的接口进行服务添加，需排除
        /// <see cref="ITransientDependency"/>、
        /// <see cref="IScopeDependency"/>、
        /// <see cref="ISingletonDependency"/>、
        /// <see cref="IDependency"/>、
        /// <see cref="IDisposable"/>等非业务接口，如无接口则注册自身
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="implementationTypes">要注册的实现类型集合</param>
        /// <param name="lifetime">注册的生命周期类型</param>
        protected virtual void AddTypeWithInterfaces(IServiceCollection services, Type[] implementationTypes,
            LifetimeStyle lifetime)
        {
            foreach (Type implementationType in implementationTypes)
            {
                if (implementationType.IsAbstract || implementationType.IsInterface)
                {
                    continue;
                }
                Type[] interfaceTypes = GetImplementedInterfaces(implementationType);
                if (interfaceTypes.Length == 0)
                {
                    services.Add(implementationType, implementationType, lifetime);
                    continue;
                }
                foreach (Type interfaceType in interfaceTypes)
                {
                    services.Add(interfaceType, implementationType, lifetime);
                }
            }
        }

        /// <summary>
        /// 重写以实现添加全局特殊类型映射
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        protected virtual void AddGlobalTypes(IServiceCollection services)
        {
            //services.AddSingleton<IAllAssemblyFinder, DirectoryAssemblyFinder>();
        }

        /// <summary>
        /// 获取指定接口的所有实现类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static Type[] GetImplementedInterfaces(Type type)
        {
            Type[] exceptInterfaces =
            {
                typeof (IDisposable),
                typeof (IDependency),
                typeof (ITransientDependency),
                typeof (IScopeDependency),
                typeof (ISingletonDependency)
            };
            Type[] interfaceTypes = type.GetInterfaces().Where(m => !exceptInterfaces.Contains(m)).ToArray();
            for (int index = 0; index < interfaceTypes.Length; index++)
            {
                Type interfaceType = interfaceTypes[index];
                if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition &&
                    interfaceType.FullName == null)
                {
                    interfaceTypes[index] = interfaceType.GetGenericTypeDefinition();
                }
            }
            return interfaceTypes;
        }
    }
}
