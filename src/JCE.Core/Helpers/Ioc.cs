using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.DependencyInjection;

namespace JCE.Core.Helpers
{
    /// <summary>
    /// 容器
    /// </summary>
    public static class Ioc
    {
        #region Property(属性)
        /// <summary>
        /// 默认容器
        /// </summary>
        private static readonly Container DefaultContainer=new Container();
        #endregion

        #region Constructor(构造函数)
        #endregion

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="configs">依赖配置</param>
        /// <returns></returns>
        public static IContainer CreateContainer(params IConfig[] configs)
        {
            var container=new Container();
            container.Register(configs);
            return container;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return DefaultContainer.Create<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object Create(Type type)
        {
            return DefaultContainer.Create(type);
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public static void Register(params IConfig[] configs)
        {
            DefaultContainer.Register(null, configs);
        }

        /// <summary>
        /// 释放容器
        /// </summary>
        public static void Dispose()
        {
            DefaultContainer.Dispose();
        }
    }
}
