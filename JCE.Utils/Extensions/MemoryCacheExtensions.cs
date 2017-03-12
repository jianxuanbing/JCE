/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：MemoryCacheExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：8303d0c8-145a-4e9c-8677-52e117130918
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:52:28
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:52:28
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 内存缓存（MemoryCache）扩展
    /// </summary>
    public static class MemoryCacheExtensions
    {
        #region Get(获取指定键值的强类型数据)
        /// <summary>
        /// 获取指定键值的强类型数据
        /// </summary>
        /// <typeparam name="T">强类型</typeparam>
        /// <param name="cache">内存缓存</param>
        /// <param name="key">缓存键值</param>
        /// <param name="regionName">区域名称，默认不支持</param>
        /// <returns></returns>
        public static T Get<T>(this MemoryCache cache, string key, string regionName = null)
        {
            object value = cache.Get(key, regionName);
            if (value is T)
            {
                return (T)value;
            }
            return default(T);
        }
        #endregion
    }
}
