using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.IocManager;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// Autofac对象容器
    /// </summary>
    public class Container:IContainer
    {
        /// <summary>
        /// 本地IOC管理器
        /// </summary>
        protected IIocManager LocalIocManager;

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            return LocalIocManager.Resolve<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public object Create(Type type)
        {            
            return LocalIocManager.Resolve(type);
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public IIocBuilder Register(params IConfig[] configs)
        {
            return Register(null, configs);
        }       

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="actionBefore">注册前操作</param>
        /// <param name="configs">依赖配置</param>
        /// <returns></returns>
        public IIocBuilder Register(Action<IIocBuilder> actionBefore,
            params IConfig[] configs)
        {
            var builder = CreateBuilder(actionBefore, configs);
            builder.CreateResolver().UseIocManager(LocalIocManager);
            return builder;
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        /// <param name="actionBefore"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        public IIocBuilder CreateBuilder(Action<IIocBuilder> actionBefore,
            params IConfig[] configs)
        {
            LocalIocManager=new IocManager();
            var builder = IocBuilder.New
                .UseAutofacContainerBuilder()
                .RegisterIocManager(LocalIocManager);
            
            actionBefore?.Invoke(builder);
            foreach (var config in configs)
            {                
                builder.RegisterModule<IConfig>(config);
            }            
            return builder;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            LocalIocManager.Resolver.Container.Dispose();
        }
    }
}
