/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ConcurrentBagExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：c035330a-4fbf-4b78-b430-57d668c1d1df
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 9:30:32
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 9:30:32
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 线程安全的无序集合（ConcurrentBag）扩展
    /// </summary>
    public static partial class ConcurrentBagExtensions
    {
        /// <summary>
        /// 清除对象的数据
        /// </summary>
        /// <typeparam name="TEntity">对象类型</typeparam>
        /// <param name="list">线程安全的无序集合</param>
        public static void Clear<TEntity>(this ConcurrentBag<TEntity> list)
        {
            TEntity entity;
            while (list.TryTake(out entity))
            {                
            }
        }
    }
}
