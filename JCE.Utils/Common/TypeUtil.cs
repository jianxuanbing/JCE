/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：TypeUtil
 * 版本号：v1.0.0.0
 * 唯一标识：24084767-b96c-48e8-9086-702c92fff5c5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/11 8:50:44
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/11 8:50:44
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 类型工具类，获取属性相关信息
    /// </summary>
    public static class TypeUtil
    {
        #region Fields(字段)
        private static readonly ConcurrentDictionary<string, object[]> AttrDict = new ConcurrentDictionary<string, object[]>();
        private static readonly ConcurrentDictionary<string, PropertyInfo[]> ProDict = new ConcurrentDictionary<string, PropertyInfo[]>();
        #endregion

        #region GetPropertyAttributes(获取属性特性)
        /// <summary>
        /// 获取属性特性
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="info">属性悉尼型</param>
        /// <param name="attributeType">特性类型</param>
        /// <returns></returns>
        public static object[] GetPropertyAttributes(string typeName, PropertyInfo info, Type attributeType)
        {
            string key = string.Concat(typeName, info.Name);
            object[] attrs;
            AttrDict.TryGetValue(key, out attrs);
            if (attrs != null)
            {
                return attrs;
            }
            attrs = info.GetCustomAttributes(attributeType, true);
            AttrDict.TryAdd(key, attrs);
            return attrs;
        }
        #endregion

        #region GetProperties(获取属性集合)
        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            PropertyInfo[] properties;
            ProDict.TryGetValue(type.FullName, out properties);
            if (properties != null)
            {
                return properties;
            }
            properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            ProDict.TryAdd(type.FullName, properties);
            return properties;
        }
        #endregion

    }
}
