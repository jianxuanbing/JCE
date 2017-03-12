/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.Mvc
 * 文件名：MvcIocResolver
 * 版本号：v1.0.0.0
 * 唯一标识：724cc926-9d4b-481d-8e12-3fd74e5bd409
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 16:28:45
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 16:28:45
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
using System.Web.Mvc;
using JCE.Core.Dependency;

namespace JCE.Autofac.Mvc
{
    /// <summary>
    /// Mvc依赖注入对象解析器
    /// </summary>
    public class MvcIocResolver:IIocResolver
    {
        /// <summary>
        /// 从全局容器中解析对象委托
        /// </summary>
        public static Func<Type,object> GlobalResolveFunc { private get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            try
            {
                return DependencyResolver.Current.GetService<T>();
            }
            catch (Exception)
            {
                if (GlobalResolveFunc != null)
                {
                    return (T) GlobalResolveFunc(typeof(T));
                }
                return default(T);
            }
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            try
            {
                return DependencyResolver.Current.GetService(type);
            }
            catch (Exception)
            {
                if (GlobalResolveFunc != null)
                {
                    return GlobalResolveFunc(type);
                }
                return null;
            }
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return DependencyResolver.Current.GetServices<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            return DependencyResolver.Current.GetServices(type);
        }
    }
}
