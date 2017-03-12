/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IIocResolver
 * 版本号：v1.0.0.0
 * 唯一标识：305f0c7d-0ab5-478e-92bc-9acb1552d889
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 15:44:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 15:44:12
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

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入对象解析获取器
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        T Resolve<T>();
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        object Resolve(Type type);
        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        IEnumerable<T> Resolves<T>();
        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IEnumerable<object> Resolves(Type type);
    }
}
