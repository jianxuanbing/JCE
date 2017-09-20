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
    /// 容器
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns></returns>
        T Create<T>();

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        object Create(Type type);

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="configs">依赖配置</param>
        IIocBuilder Register(params IConfig[] configs);

        /// <summary>
        /// 释放资源
        /// </summary>
        void Dispose();
    }
}
