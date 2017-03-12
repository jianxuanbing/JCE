/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.LocalApp.Initialize
 * 文件名：LocalIocResolver
 * 版本号：v1.0.0.0
 * 唯一标识：19273757-5312-4183-9b23-945abab3e164
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 13:50:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 13:50:38
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
using Autofac;
using JCE.Core.Dependency;

namespace JCE.Autofac.LocalApp.Initialize
{
    /// <summary>
    /// 本地应用-依赖注入对象解析获取器
    /// </summary>
    public class LocalIocResolver:IIocResolver
    {
        /// <summary>
        /// 获取 依赖注入容器
        /// </summary>
        internal static IContainer Container { get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return Container.ResolveOptional(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Container.ResolveOptional<IEnumerable<T>>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            Type typeToResolve = typeof(IEnumerable<>).MakeGenericType(type);
            Array array = Container.ResolveOptional(typeToResolve) as Array;
            if (array != null)
            {
                return array.Cast<object>();
            }
            return new object[0];
        }
    }
}
