using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// Autofac 扩展
    /// </summary>
    public static partial class IocExtensions
    {
        /// <summary>
        /// 注册服务，生命周期为 InstancePerLifetimeScope（每个请求一个实例）
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddScoped<TService, TImplementation>(this ContainerBuilder builder, string name = null)
            where TService : class where TImplementation : class, TService
        {
            if (name == null)
            {
                return builder.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
            }
            return builder.RegisterType<TImplementation>().Named<TService>(name).InstancePerLifetimeScope();
        }

        /// <summary>
        /// 注册服务，生命周期为 SingleInstance（单例）
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddSingleton<TService, TImplementation>(this ContainerBuilder builder, string name = null)
            where TService : class where TImplementation : class, TService
        {
            if (name == null)
            {
                return builder.RegisterType<TImplementation>().As<TService>().SingleInstance();
            }
            return builder.RegisterType<TImplementation>().Named<TService>(name).SingleInstance();
        }

        /// <summary>
        /// 注册服务，生命周期为 SingleInstance（单例）
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="instance">服务实例</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle>
            AddSingleton<TService, TImplementation>(this ContainerBuilder builder, TImplementation instance)
            where TService : class where TImplementation : class, TService
        {
            return builder.RegisterInstance(instance).As<TService>().SingleInstance();
        }
    }
}
