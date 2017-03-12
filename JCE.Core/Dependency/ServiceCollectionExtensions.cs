/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServiceCollectionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：39293795-5dd2-4e15-943a-6d1acba36768
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 15:07:10
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 15:07:10
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 服务集合扩展辅助操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region AddTransient(注册即时生命周期类型的映射信息)
        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient(this IServiceCollection collection, Type serviceType,
            Type implementationType)
        {
            return Add(collection, serviceType, implementationType, LifetimeStyle.Transient);
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient(this IServiceCollection collection, Type serviceType,
            Func<IServiceProvider, object> factory)
        {
            return Add(collection, serviceType, factory, LifetimeStyle.Transient);
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient(this IServiceCollection collection, Type serviceType)
        {
            return collection.AddTransient(serviceType, serviceType);
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddTransient(typeof (TService), typeof (TImplementation));
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.AddTransient(typeof (TService));
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient<TService>(this IServiceCollection collection,
            Func<IServiceProvider, TService> factory) where TService : class
        {
            return collection.AddTransient(typeof (TService), factory);
        }

        /// <summary>
        /// 注册即时生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection collection,
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddTransient(typeof (TService), factory);
        }
	    #endregion

        #region AddScoped(注册范围生命周期类型的映射信息)
        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped(this IServiceCollection collection, Type serviceType,
            Type implementationType)
        {
            return Add(collection, serviceType, implementationType, LifetimeStyle.Scoped);
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped(this IServiceCollection collection, Type serviceType,
            Func<IServiceProvider, object> factory)
        {
            return Add(collection, serviceType, factory, LifetimeStyle.Scoped);
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped(this IServiceCollection collection, Type serviceType)
        {
            return collection.AddScoped(serviceType, serviceType);
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.AddScoped(typeof(TService));
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped<TService>(this IServiceCollection collection,
            Func<IServiceProvider, TService> factory) where TService : class
        {
            return collection.AddScoped(typeof(TService), factory);
        }

        /// <summary>
        /// 注册范围生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection collection,
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddScoped(typeof(TService), factory);
        }
        #endregion

        #region AddSingleton(注册单例生命周期类型的映射信息)
        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton(this IServiceCollection collection, Type serviceType,
            Type implementationType)
        {
            return Add(collection, serviceType, implementationType, LifetimeStyle.Singleton);
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton(this IServiceCollection collection, Type serviceType,
            Func<IServiceProvider, object> factory)
        {
            return Add(collection, serviceType, factory, LifetimeStyle.Singleton);
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton(this IServiceCollection collection, Type serviceType)
        {
            return collection.AddSingleton(serviceType, serviceType);
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.AddSingleton(typeof(TService));
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton<TService>(this IServiceCollection collection,
            Func<IServiceProvider, TService> factory) where TService : class
        {
            return collection.AddSingleton(typeof(TService), factory);
        }

        /// <summary>
        /// 注册单例生命周期类型的映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection collection,
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return collection.AddSingleton(typeof(TService), factory);
        }
        #endregion

        #region AddInstance(注册单例生命周期的实例映射信息)
        /// <summary>
        /// 注册单例生命周期的实例映射信息
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="instance">服务实现类型实例</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddInstance<TService>(this IServiceCollection collection, TService instance)
            where TService : class
        {
            return collection.AddInstance(typeof (TService), instance);
        }
        /// <summary>
        /// 注册单例生命周期的实例映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">服务实现类型实例</param>
        /// <returns>服务映射信息集合</returns>
        public static IServiceCollection AddInstance(this IServiceCollection collection, Type serviceType,
            object instance)
        {
            ServiceDescriptor descriptor=new ServiceDescriptor(serviceType,instance);
            return collection.TryAdd(descriptor);
        }
        #endregion

        #region Add(注册指定生命周期类型的映射信息)
        /// <summary>
        /// 注册指定生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <param name="lifetime">生命周期类型</param>
        /// <returns></returns>
        public static IServiceCollection Add(this IServiceCollection collection, Type serviceType,
            Type implementationType, LifetimeStyle lifetime)
        {
            ServiceDescriptor descriptor=new ServiceDescriptor(serviceType,implementationType,lifetime);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 注册指定生命周期类型的映射信息
        /// </summary>
        /// <param name="collection">服务映射信息集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">实例工厂</param>
        /// <param name="lifetime">生命周期类型</param>
        /// <returns></returns>
        private static IServiceCollection Add(IServiceCollection collection, Type serviceType,
            Func<IServiceProvider, object> factory, LifetimeStyle lifetime)
        {
            ServiceDescriptor descriptor=new ServiceDescriptor(serviceType,factory,lifetime);
            return collection.TryAdd(descriptor);
        }
        #endregion

        #region AddDescriptor(将映射描述直接添加到服务映射集合中)
        /// <summary>
        /// 将映射描述直接添加到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="descriptor">服务映射信息</param>
        /// <returns></returns>
        public static IServiceCollection AddDescriptor(this IServiceCollection collection, ServiceDescriptor descriptor)
        {
            Type implementationType = descriptor.GetImplementationType();
            if (implementationType == typeof (object) || implementationType == descriptor.ServiceType)
            {
                throw new InvalidOperationException(
                    "实现类型不能为\"{0}\"，因为该类型与注册为\"{1}\"的其他类型无法区分".FormatWith(implementationType,
                        descriptor.ServiceType));
            }
            if (
                !collection.Any(
                    m => m.ServiceType == descriptor.ServiceType && m.GetImplementationType() == implementationType))
            {
                collection.Add(descriptor);
            }
            return collection;
        }

        /// <summary>
        /// 将映射描述直接添加到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="descriptors">多个服务映射信息</param>
        /// <returns></returns>
        public static IServiceCollection AddDescriptors(this IServiceCollection collection,
            IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (ServiceDescriptor descriptor in descriptors)
            {
                collection.Add(descriptor);
            }
            return collection;
        }
        #endregion

        #region TryAdd(尝试将映射描述添加到服务映射集合中，存在则替换并后移)
        /// <summary>
        /// 尝试将映射描述添加到服务映射集合中，存在则替换并后移
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="descriptor">服务映射信息</param>
        /// <returns></returns>
        public static IServiceCollection TryAdd(this IServiceCollection collection, ServiceDescriptor descriptor)
        {
            ServiceDescriptor exist =
                collection.FirstOrDefault(
                    m =>
                        m.ServiceType == descriptor.ServiceType && m.ImplementationType == descriptor.ImplementationType);
            if (exist == null)
            {
                collection.Add(descriptor);
            }
            else
            {
                collection.Remove(exist);
                collection.Add(descriptor);
            }
            return collection;
        }

        /// <summary>
        /// 尝试将多个映射描述添加到服务映射集合中，存在则替换并后移
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="descriptors">多个服务映射信息</param>
        /// <returns></returns>
        public static IServiceCollection TryAdd(this IServiceCollection collection,
            IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (ServiceDescriptor descriptor in descriptors)
            {
                collection.TryAdd(descriptor);
            }
            return collection;
        }
        #endregion

        #region TryAddTransient(创建即时类型的映射并尝试到服务映射集合中)
        /// <summary>
        /// 创建即时类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddTransient(this IServiceCollection collection, Type serviceType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Transient(serviceType, serviceType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建即时类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddTransient(this IServiceCollection collection, Type serviceType, Type implementationType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Transient(serviceType, implementationType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建即时类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns></returns>
        public static IServiceCollection TryAddTransient(this IServiceCollection collection, Type serviceType, Func<IServiceProvider, object> factory)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Transient(serviceType, factory);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建即时类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddTransient<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddTransient(typeof(TService));
        }

        /// <summary>
        /// 创建即时类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddTransient<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddTransient(typeof(TService), typeof(TImplementation));
        }
        #endregion

        #region TryAddScoped(创建范围类型的映射并尝试到服务映射集合中)
        /// <summary>
        /// 创建范围类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddScoped(this IServiceCollection collection, Type serviceType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Scoped(serviceType, serviceType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建范围类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddScoped(this IServiceCollection collection, Type serviceType, Type implementationType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Scoped(serviceType, implementationType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建范围类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns></returns>
        public static IServiceCollection TryAddScoped(this IServiceCollection collection, Type serviceType, Func<IServiceProvider, object> factory)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Scoped(serviceType, factory);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建范围类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddScoped<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddScoped(typeof(TService));
        }

        /// <summary>
        /// 创建范围类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddScoped<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddScoped(typeof(TService), typeof(TImplementation));
        }
        #endregion

        #region TryAddSingleton(创建单例类型的映射并尝试到服务映射集合中)
        /// <summary>
        /// 创建单例类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddSingleton(this IServiceCollection collection, Type serviceType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Singleton(serviceType, serviceType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建单例类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <returns></returns>
        public static IServiceCollection TryAddSingleton(this IServiceCollection collection, Type serviceType, Type implementationType)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Singleton(serviceType, implementationType);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建单例类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实现类型实例工厂</param>
        /// <returns></returns>
        public static IServiceCollection TryAddSingleton(this IServiceCollection collection, Type serviceType, Func<IServiceProvider, object> factory)
        {
            ServiceDescriptor descriptor = ServiceDescriptor.Singleton(serviceType, factory);
            return collection.TryAdd(descriptor);
        }

        /// <summary>
        /// 创建单例类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddSingleton(typeof(TService));
        }

        /// <summary>
        /// 创建单例类型的映射并尝试到服务映射集合中
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <returns></returns>
        public static IServiceCollection TryAddSingleton<TService, TImplementation>(this IServiceCollection collection)
            where TService : class
        {
            return collection.TryAddSingleton(typeof(TService), typeof(TImplementation));
        }
        #endregion

        #region Replace(替换服务集合中已经存在的映射信息)
        /// <summary>
        /// 替换服务集合中已经存在的映射信息
        /// </summary>
        /// <param name="collection">服务映射集合</param>
        /// <param name="descriptor">映射信息</param>
        /// <returns></returns>
        public static IServiceCollection Replace(this IServiceCollection collection, ServiceDescriptor descriptor)
        {
            ServiceDescriptor registedDescriptor =
                collection.FirstOrDefault(m => m.ServiceType == descriptor.ServiceType);
            if (registedDescriptor != null)
            {
                collection.Remove(registedDescriptor);
            }
            collection.Add(descriptor);
            return collection;
        }
        #endregion
    }
}
