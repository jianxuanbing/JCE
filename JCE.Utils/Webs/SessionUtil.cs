/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webs
 * 文件名：SessionUtil
 * 版本号：v1.0.0.0
 * 唯一标识：c4053dd8-6e99-4f84-a370-c77b1d9a3d3c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/22 23:25:15
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/22 23:25:15
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
using System.Web;
using JCE.Utils.Extensions;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// Session工具类
    /// </summary>
    public class SessionUtil
    {
        #region Set(设置Session)
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void Set<T>(string key, T value)
        {
            Set(key, value, 30, 0);
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void Set(string key, string value)
        {
            Set<string>(key, value);
        }
        /// <summary>
        /// 设置Session,并调整有效期为分钟或年
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        /// <param name="expires">分钟数,大于0则以分钟数为有效期，等于0则以后面的年为有效期</param>
        /// <param name="year">年数,当分钟数为0时按年数为有效期，当分钟数大于0时此参数随意设置</param>
        public static void Set(string key, string value, int expires, int year)
        {
            Set<string>(key, value, expires, year);
        }
        /// <summary>
        /// 设置Session,并调整有效期为分钟或年
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        /// <param name="expires">分钟数,大于0则以分钟数为有效期，等于0则以后面的年为有效期</param>
        /// <param name="year">年数,当分钟数为0时按年数为有效期，当分钟数大于0时此参数随意设置</param>
        public static void Set<T>(string key, T value, int expires, int year)
        {
            if (key.IsEmpty())
            {
                return;
            }
            HttpContext.Current.Session[key] = value;
            if (expires > 0)
            {
                HttpContext.Current.Session.Timeout = expires;
            }
            else if (year > 0)
            {
                HttpContext.Current.Session.Timeout = 60 * 24 * 365 * year;
            }
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="values">Session的键值</param>
        public static void Set<T>(string key, T[] values)
        {
            Set(key, values, 20);
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="values">Session的键值</param>
        /// <param name="expires">分钟数,大于0则以分钟数为有效期</param>
        public static void Set<T>(string key, T[] values, int expires)
        {
            if (key.IsEmpty())
            {
                return;
            }
            HttpContext.Current.Session[key] = values;
            if (expires > 0)
            {
                HttpContext.Current.Session.Timeout = expires;
            }
        }
        #endregion

        #region Get(读取Session)
        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (key.IsEmpty())
            {
                return default(T);
            }
            return Conv.To<T>(HttpContext.Current.Session[key]);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <returns></returns>
        public static string Get(string key)
        {
            if (key.IsEmpty())
            {
                return string.Empty;
            }
            return HttpContext.Current.Session[key] as string;
        }

        /// <summary>
        /// 读取Session对象值数组
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <returns></returns>
        public static T[] Gets<T>(string key)
        {
            if (key.IsEmpty())
            {
                return default(T[]);
            }
            return HttpContext.Current.Session[key] as T[];
        }

        /// <summary>
        /// 读取Session对象值数组
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <returns></returns>
        public static string[] Gets(string key)
        {
            return Gets<string>(key);
        }
        #endregion

        #region Remove(删除指定Session)
        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void Remove(string key)
        {
            if (key.IsEmpty())
            {
                return;
            }
            HttpContext.Current.Session.Remove(key);
        }
        #endregion

        #region Clear(清空所有Session)
        /// <summary>
        /// 清空所有Session
        /// </summary>
        public static void Clear()
        {            
            HttpContext.Current.Session.Clear();
        }
        #endregion
    }
}
