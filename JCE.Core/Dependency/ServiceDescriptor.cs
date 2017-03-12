/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServiceDescriptor
 * 版本号：v1.0.0.0
 * 唯一标识：06647074-a2ed-4e00-a6c5-c19064e6b482
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 16:07:02
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 16:07:02
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入映射描述信息
    /// </summary>
    [DebuggerDisplay("Lifetime={Lifetime},ServiceType={ServiceType},ImplementationType={ImplementationType}")]
    public class ServiceDescriptor
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个<see cref="ServiceDescriptor"/>类型的新实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <param name="lifetime">生命周期</param>
        public ServiceDescriptor(Type serviceType, Type implementationType, LifetimeStyle lifetime)
            : this(serviceType, lifetime)
        {
            ImplementationType = implementationType;
        }
        /// <summary>
        /// 初始化一个<see cref="ServiceDescriptor"/>类型的新实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">服务实例</param>
        public ServiceDescriptor(Type serviceType, object instance) : this(serviceType, LifetimeStyle.Singleton)
        {
            ImplementationInstance = instance;
        }
        /// <summary>
        /// 初始化一个<see cref="ServiceDescriptor"/>类型的新实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实例工厂</param>
        /// <param name="lifetime">生命周期</param>
        public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory, LifetimeStyle lifetime)
            : this(serviceType, lifetime)
        {
            ImplementationFactory = factory;
        }
        /// <summary>
        /// 初始化一个<see cref="ServiceDescriptor"/>类型的新实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="lifetime">生命周期</param>
        public ServiceDescriptor(Type serviceType, LifetimeStyle lifetime)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取生命周期的描述
        /// </summary>
        public LifetimeStyle Lifetime { get; private set; }
        /// <summary>
        /// 获取服务类型
        /// </summary>
        public Type ServiceType { get; private set; }
        /// <summary>
        /// 获取服务实现类型
        /// </summary>
        public Type ImplementationType { get; private set; }
        /// <summary>
        /// 获取服务实例
        /// </summary>
        public object ImplementationInstance { get; private set; }
        /// <summary>
        /// 获取服务实例创建工厂
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; private set; }
        #endregion

        #region Transient(创建即时生命周期类型的描述)
        /// <summary>
        /// 创建即时生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <returns></returns>
        public static ServiceDescriptor Transient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(typeof (TService), typeof (TImplementation), LifetimeStyle.Transient);
        }
        /// <summary>
        /// 创建即时生命周期类型的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实例类型</param>
        /// <returns></returns>
        public static ServiceDescriptor Transient(Type serviceType, Type implementationType)
        {
            return Describe(serviceType, implementationType, LifetimeStyle.Transient);
        }

        /// <summary>
        /// 创建即时生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Transient<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(typeof (TService), typeof (TImplementation), LifetimeStyle.Transient);
        }

        /// <summary>
        /// 创建即时生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Transient<TService>(Func<IServiceProvider, TService> factory)
            where TService : class
        {
            return Describe(typeof (TService), factory, LifetimeStyle.Transient);
        }

        /// <summary>
        /// 创建即时生命周期类型的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Transient(Type serviceType, Func<IServiceProvider, object> factory)
        {
            return Describe(serviceType, factory, LifetimeStyle.Transient);
        }
        #endregion

        #region Scope(创建局部生命周期类型的描述)
        /// <summary>
        /// 创建局部生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <returns></returns>
        public static ServiceDescriptor Scoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(LifetimeStyle.Scoped);
        }
        /// <summary>
        /// 创建局部生命周期类型的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实例类型</param>
        /// <returns></returns>
        public static ServiceDescriptor Scoped(Type serviceType, Type implementationType)
        {
            return Describe(serviceType, implementationType, LifetimeStyle.Scoped);
        }
        /// <summary>
        /// 创建局部生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Scoped<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(typeof (TService), factory, LifetimeStyle.Scoped);
;        }
        /// <summary>
        /// 创建局部生命周期类型的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> factory)
            where TService : class
        {
            return Describe(typeof (TService), factory, LifetimeStyle.Scoped);
        }
        /// <summary>
        /// 创建局部生命周期类型的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Scoped(Type serviceType, Func<IServiceProvider, object> factory)
        {
            return Describe(serviceType, factory, LifetimeStyle.Scoped);
        }
        #endregion

        #region Singleton(创建单例生命周期实例的描述)
        /// <summary>
        /// 创建单例生命周期实例的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <returns></returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(LifetimeStyle.Singleton);
        }
        /// <summary>
        /// 创建单例生命周期实例的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">服务实例类型</param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton(Type serviceType, Type implementationType)
        {
            return Describe(serviceType, implementationType, LifetimeStyle.Singleton);
        }
        /// <summary>
        /// 创建单例生命周期实例的描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实例类型</typeparam>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(typeof(TService), factory, LifetimeStyle.Singleton);
        }        

        /// <summary>
        /// 创建单例生命周期实例描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">实例工厂</param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton(Type serviceType, Func<IServiceProvider, object> factory)
        {
            return Describe(serviceType, factory, LifetimeStyle.Singleton);
        }

        /// <summary>
        /// 创建单例生命周期实例描述
        /// </summary>
        /// <typeparam name="TServer">服务类型</typeparam>
        /// <param name="instance">服务实例</param>
        /// <returns></returns>
        public static ServiceDescriptor Instance<TServer>(TServer instance) where TServer : class
        {
            return Instance(typeof (TServer), instance);
        }

        /// <summary>
        /// 创建单例生命周期实例的描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">服务实例</param>
        /// <returns></returns>
        public static ServiceDescriptor Instance(Type serviceType, object instance)
        {
            return Describe(serviceType, instance);
        }
        #endregion

        #region Describe(描述)
        /// <summary>
        /// 获取服务描述
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementation">服务实现类型</typeparam>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        private static ServiceDescriptor Describe<TService, TImplementation>(LifetimeStyle lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(typeof (TService), typeof (TImplementation), lifetime);
        }
        /// <summary>
        /// 获取服务描述
        /// </summary>
        /// <param name="serviceType">描述类型</param>
        /// <param name="implementationType">服务实现类型</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        private static ServiceDescriptor Describe(Type serviceType, Type implementationType, LifetimeStyle lifetime)
        {
            return new ServiceDescriptor(serviceType,implementationType,lifetime);
        }

        /// <summary>
        /// 获取服务描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="factory">服务实例工厂</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        private static ServiceDescriptor Describe(Type serviceType, Func<IServiceProvider, object> factory,
            LifetimeStyle lifetime)
        {
            return new ServiceDescriptor(serviceType,factory,lifetime);
        }

        /// <summary>
        /// 获取服务描述
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">服务实例</param>
        /// <returns></returns>
        private static ServiceDescriptor Describe(Type serviceType, object instance)
        {
            return new ServiceDescriptor(serviceType,instance);
        }
        #endregion

        #region GetImplementationType(获取实例类型)
        /// <summary>
        /// 获取实例类型
        /// </summary>
        /// <returns></returns>
        internal Type GetImplementationType()
        {
            if (ImplementationType != null)
            {
                return ImplementationType;
            }
            if (ImplementationInstance != null)
            {
                return ImplementationInstance.GetType();
            }
            if (ImplementationFactory != null)
            {
                Type[] typeArgs = ImplementationFactory.GetType().GenericTypeArguments;
                if (typeArgs.Length == 2)
                {
                    return typeArgs[1];
                }
            }
            throw new ArgumentException("类型\"{0}\" 的实现类型无法找到".FormatWith(ServiceType));
        }
        #endregion
    }
}
