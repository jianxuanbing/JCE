/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：CacheUtil
 * 版本号：v1.0.0.0
 * 唯一标识：f46d445c-b4c7-48b5-a12a-fdbfb98524fd
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 22:35:55
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 22:35:55
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JCE.Utils.Common
{
    /// <summary>
    /// ASP.NET缓存工具类
    /// </summary>
    public class CacheUtil
    {
        #region Get(获取数据缓存)
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public static object Get(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }
        #endregion

        #region Set(设置数据缓存)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="objValue">对象值</param>
        public static void Set(string cacheKey, object objValue)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objValue);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="objValue">对象值</param>
        /// <param name="timeout">超时时间</param>
        public static void Set(string cacheKey, object objValue, TimeSpan timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objValue, null, DateTime.MaxValue, timeout,
                System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="objValue">对象值</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public static void Set(string cacheKey, object objValue, DateTime absoluteExpiration,
            TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objValue, null, absoluteExpiration, slidingExpiration);
        }
        #endregion

        #region Remove(移除数据缓存)
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public static void Remove(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAll()
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = objCache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                objCache.Remove(cacheEnum.Key.ToString());
            }
        }
        #endregion
    }
}
