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

        public T Create<T>()
        {
            return LocalIocManager.Resolve<T>();
        }

        public object Create(Type type)
        {
            return LocalIocManager.Resolve(type);
        }

        public void Register(params IConfig[] configs)
        {
            Register(null, null, configs);
        }       

        public IIocBuilder Register(IServiceRegistration services, params IConfig[] configs)
        {
            return Register(services, null, configs);
        }

        public IIocBuilder Register(IServiceRegistration services, Action<IServiceRegistration> actionBefore,
            params IConfig[] configs)
        {
            var builder = CreateBuilder(services, actionBefore, configs);
            builder.CreateResolver().UseIocManager(LocalIocManager);
            return builder;
        }

        public IIocBuilder CreateBuilder(IServiceRegistration services,Action<IServiceRegistration> actionBefore,
            params IConfig[] configs)
        {
            LocalIocManager=new IocManager();
            var builder = IocBuilder.New
                .UseAutofacContainerBuilder()
                .RegisterIocManager(LocalIocManager);
            
            actionBefore?.Invoke(services);
            foreach (var config in configs)
            {                
                builder.RegisterModule<IConfig>(config);
            }
            return builder;
        }

        public void Dispose()
        {
            
        }
    }
}
