/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServiceProviderExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：13f388d1-b617-4e92-b465-84bcbff1d509
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 15:52:15
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 15:52:15
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 服务提供者扩展辅助操作
    /// </summary>
    public static class ServiceProviderExtensions
    {
        #region GetService(获取指定类型服务的实例)
        /// <summary>
        /// 获取指定类型服务的实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的实例</returns>
        public static T GetService<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException("框架尚未初始化，请先初始化");
            }
            return (T)provider.GetService(typeof(T));
        }
        #endregion

        #region GetRequiredService(获取指定类型服务的非空实例)
        /// <summary>
        /// 获取指定类型服务的非空实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="serviceType">要获取实例的服务类型</param>
        /// <returns>指定类型的非空实例</returns>
        public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new InvalidOperationException("框架尚未初始化，请先初始化");
            }
            object value = provider.GetService(serviceType);
            if (value == null)
            {
                throw new InvalidOperationException("类型\"{0}\"的实现类型无法找到".FormatWith(serviceType));
            }
            return value;
        }

        /// <summary>
        /// 获取指定类型服务的非空实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的非空实例</returns>
        public static T GetRequiredService<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException("框架尚未初始化，请先初始化");
            }
            return (T) GetRequiredService(provider, typeof (T));
        }
        #endregion

        #region GetServices(获取指定类型服务的所有实例)
        /// <summary>
        /// 获取指定类型服务的所有实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的所有实例</returns>
        public static IEnumerable<T> GetServices<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException("框架尚未初始化，请先初始化");
            }
            return provider.GetRequiredService<IEnumerable<T>>();
        }

        /// <summary>
        /// 获取指定类型服务的所有实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="serviceType">要获取实例的服务类型</param>
        /// <returns>指定类型的所有实例</returns>
        public static IEnumerable<object> GetServices(this IServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new InvalidOperationException("框架尚未初始化，请先初始化");
            }
            Type genericEnumerable = typeof (IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>) provider.GetRequiredService(genericEnumerable);
        }
        #endregion
    }
}
