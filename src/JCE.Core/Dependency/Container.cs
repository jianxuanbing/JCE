using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Integration.WebApi;

namespace JCE.Core.Dependency
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
        /// 创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public List<T> CreatList<T>(string name = null)
        {
            var result = CreatList(typeof(T), name);
            if (result == null)
            {
                return new List<T>();
            }
            return ((IEnumerable<T>) result).ToList();
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object CreatList(Type type, string name = null)
        {
            Type serviceType = typeof(IEnumerable<>).MakeGenericType(type);
            return Create(serviceType, name);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public T Create<T>(string name=null)
        {
            return (T) Create(typeof(T), name);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object Create(Type type,string name=null)
        {            
            return HttpContext.Current != null ? GetServiceFromHttpContext(type, name) : GetService(type, name);
        }

        /// <summary>
        /// 从HttpContext获取服务
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        private object GetServiceFromHttpContext(Type type, string name)
        {
            if (name == null)
            {
                return _webapiResolver.GetService(type);
            }
            var context = _webapiResolver.Container.ResolveNamed(name, type);
            return context;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        private object GetService(Type type, string name)
        {
            if (name == null)
            {
                return _container.Resolve(type);
            }
            return _container.ResolveNamed(name, type);
        }

        /// <summary>
        /// 作用域开始
        /// </summary>
        /// <returns></returns>
        public IScope BeginScope()
        {
            return new Scope(_container.BeginLifetimeScope());
        }

        public void Register(params IConfig[] configs)
        {
            Register(null,configs);
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="assembly">项目所在的程序集</param>
        /// <param name="configs">依赖配置</param>
        public void Register(Assembly assembly, params IConfig[] configs)
        {
            Register(assembly,null,configs);
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
            RegisterAop(builder);
            if (assembly != null)
            {
                builder.RegisterAssemblyTypes(assembly);
            }            
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
        /// 注册AOP
        /// </summary>
        /// <param name="builder">容器生成器</param>
        private void RegisterAop(ContainerBuilder builder)
        {
            builder.RegisterDynamicProxy(config => config.EnableParameterAspect());
            //builder.EnableAspectScoped();
        }

        public void Init(Action<ContainerBuilder> action, params IConfig[] configs)
        {
            var builder = CreateBuilder(action, configs);
            _container = builder.Build();
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
