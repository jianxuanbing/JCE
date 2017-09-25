using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// 容器
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        T Create<T>(string name=null);

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        object Create(Type type,string name=null);

        /// <summary>
        /// 作用域开始
        /// </summary>
        /// <returns></returns>
        IScope BeginScope();

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="assembly">项目所在的程序集</param>
        /// <param name="configs">依赖配置</param>
        void Register(Assembly assembly, params IConfig[] configs);

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="assembly">项目所在的程序集</param>
        /// <param name="action">在注册模块前执行的操作</param>
        /// <param name="configs">依赖配置</param>
        void Register(Assembly assembly, Action<ContainerBuilder> action, params IConfig[] configs);

        void Init(Action<ContainerBuilder> action, params IConfig[] configs);
    }
}
