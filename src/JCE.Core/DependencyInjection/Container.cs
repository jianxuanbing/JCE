using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// Autofac对象容器
    /// </summary>
    internal class Container:IContainer
    {
        /// <summary>
        /// 容器
        /// </summary>
        private Autofac.IContainer _container;

        /// <summary>
        /// WebApi 依赖解析器
        /// </summary>
        private AutofacWebApiDependencyResolver _webapiResolver;

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public object Create(Type type)
        {            
            return _container.Resolve(type);
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="assembly">项目所在的程序集</param>
        /// <param name="action">在注册模块前执行的操作</param>
        /// <param name="configs">依赖配置</param>
        public void Register(Assembly assembly, Action<ContainerBuilder> action, params IConfig[] configs)
        {
            var config= GlobalConfiguration.Configuration;
            var builder = CreateBuilder(action, configs);
            builder.RegisterApiControllers(assembly);
            builder.RegisterWebApiFilterProvider(config);
            _container = builder.Build();
            _webapiResolver=new AutofacWebApiDependencyResolver(_container);
            config.DependencyResolver = _webapiResolver;
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        /// <param name="action"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        public ContainerBuilder CreateBuilder(Action<ContainerBuilder> action, params IConfig[] configs)
        {
            var builder=new ContainerBuilder();
            action?.Invoke(builder);
            foreach (var config in configs)
            {
                builder.RegisterModule(config);
            }
            return builder;
        }        

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
