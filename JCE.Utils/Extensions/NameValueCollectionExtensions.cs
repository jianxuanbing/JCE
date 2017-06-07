/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：NameValueCollectionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：d9ccb6f6-557a-4e02-b994-ba325c6e2da5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/4/18 22:39:06
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/4/18 22:39:06
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 键值对集合（NameValueCollection）扩展
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// 将键值对集合转换成字典
        /// </summary>
        /// <param name="source">键值对集合</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            if (source != null)
            {
                Dictionary<string,string> dict=new Dictionary<string, string>();
                foreach (string key in source.AllKeys)
                {
                    dict.Add(key,source[key]);
                }
                return dict;
            }
            return null;
        }

        /// <summary>
        /// 将键值对集合转换成查询字符串
        /// </summary>
        /// <param name="source">键值对集合</param>
        /// <param name="valueFunc">值操作</param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection source,Func<NameValueCollection,string,string> valueFunc=null)
        {
            if (source != null)
            {
                Str sb=new Str();
                foreach (string key in source.AllKeys)
                {
                    if (valueFunc != null)
                    {
                        sb.Append("{0}={1}&", key, valueFunc(source, source[key]));
                    }
                    else
                    {
                        sb.Append("{0}={1}&", key, source[key]);
                    }                    
                }
                sb.RemoveEnd("&");
                return sb.ToString();
            }
            return string.Empty;
        }        
    }
}
