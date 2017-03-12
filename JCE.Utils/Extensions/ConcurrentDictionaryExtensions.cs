/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ConcurrentDictionaryExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：2019f0b4-16ff-4a71-a509-bce825912a7b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/24 9:55:32
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/24 9:55:32
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
    /// ConcurrentDictionary线程安全集合扩展
    /// </summary>
    public static class ConcurrentDictionaryExtensions
    {
        #region Remove(移除字典项)
        /// <summary>
        /// 移除字典项，指定键
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        public static void Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            dictionary.TryRemove(key, out value);
        }
        #endregion
        
    }
}
