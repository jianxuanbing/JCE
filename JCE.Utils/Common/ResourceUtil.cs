/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：ResourceUtil
 * 版本号：v1.0.0.0
 * 唯一标识：8352e5c8-5677-4687-b5af-b1fec8d3bcad
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:45:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:45:08
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 资源操作工具类
    /// </summary>
    public class ResourceUtil
    {
        #region GetString(获取资源文件中的字符串)
        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名，应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetString(string resourceName, string key)
        {
            return GetString(resourceName, key, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名，应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static string GetString(string resourceName, string key, string assemblyName)
        {
            return GetString(resourceName, key, Reflection.GetAssembly(assemblyName));
        }

        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名，应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static string GetString(string resourceName, string key, Assembly assembly)
        {
            ValidateGetString(resourceName, key);
            string result = GetResourceStringFormAssembly(resourceName, key, assembly);
            if (!result.IsEmpty())
            {
                return result;
            }
            assembly = Assembly.GetExecutingAssembly();
            return GetResourceStringFormAssembly(resourceName, key, assembly);
        }
        #endregion

        /// <summary>
        /// 验证获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名</param>
        /// <param name="key">资源值</param>
        private static void ValidateGetString(string resourceName, string key)
        {
            if (resourceName.IsEmpty())
            {
                throw new ArgumentNullException("resourceName");
            }
            if (key.IsEmpty())
            {
                throw new ArgumentNullException("key");
            }
        }
        /// <summary>
        /// 从资源中获取字符串
        /// </summary>
        /// <param name="resourceName">资源名</param>
        /// <param name="key">资源值</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static string GetResourceStringFormAssembly(string resourceName, string key, Assembly assembly)
        {
            string result = GetStringByManager(resourceName, key, assembly);
            if (!result.IsEmpty())
            {
                return result;
            }
            return GetStringByManager(GetResourceFullName(resourceName, assembly), key, assembly);
        }
        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名</param>
        /// <param name="key">资源值</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        private static string GetStringByManager(string resourceName, string key, Assembly assembly)
        {
            try
            {
                var manager=new ResourceManager(resourceName,assembly);
                return manager.GetString(key);
            }
            catch (MissingManifestResourceException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取资源名的全名
        /// </summary>
        /// <param name="resourceName">资源名</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        private static string GetResourceFullName(string resourceName, Assembly assembly)
        {
            string[] resources = assembly.GetManifestResourceNames();
            const string extension = ".resources";
            foreach (var resource in resources)
            {
                if (resource.EndsWith(string.Format("{0}{1}", resourceName, extension)))
                {
                    return resource.Replace(extension, "");
                }
            }
            return string.Empty;
        }
    }
}
