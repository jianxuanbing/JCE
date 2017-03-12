/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：ConfigUtil
 * 版本号：v1.0.0.0
 * 唯一标识：239ee1ce-c2f9-4b55-9b23-3ed1bd8a351e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/10 23:06:59
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/10 23:06:59
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using JCE.Utils.Extensions;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 配置信息工具类
    /// </summary>
    public static partial class ConfigUtil
    {
        #region GetAppSettings(获取AppSettings)
        /// <summary>
        /// 获取AppSettings
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            key.CheckNotNull("key");
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

        #region SetAppSettings(设置AppSettings)
        /// <summary>
        /// 设置AppSettings
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public static void SetAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;
            if (settings.AllKeys.Contains(key))
            {
                settings[key].Value = value;
            }
            else
            {
                settings.Add(key,value);
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        #region RemoceAppSettings(移除AppSettings)
        /// <summary>
        /// 移除AppSettings
        /// </summary>
        /// <param name="key">键名</param>
        public static void RemoveAppSettings(string key)
        {
            if (key.IsEmpty())
            {
                return;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings == null||config.AppSettings.Settings[key]==null)
            {
                return;
            }
            config.AppSettings.Settings.Remove(key);
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        #region GetConnectionString(获取数据库连接字符串)
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            key.CheckNotNull("key");
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        #endregion

        #region SetConnectionString(设置数据库连接字符串)
        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="connectionStr">数据库连接字符串</param>
        /// <param name="providerName">数据提供程序名称</param>
        public static void SetConnectionString(string key, string connectionStr, string providerName = "")
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionSettings = config.ConnectionStrings.ConnectionStrings;
            if (connectionSettings[key] != null)
            {
                connectionSettings[key].ConnectionString = connectionStr;
                if (!string.IsNullOrEmpty(providerName))
                {
                    connectionSettings[key].ProviderName = providerName;
                }
            }
            else
            {
                connectionSettings.Add(new ConnectionStringSettings()
                {
                    Name = key,
                    ConnectionString = connectionStr,
                    ProviderName = providerName
                });
            }
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        #endregion

        #region GetProviderName(获取数据提供程序名称)
        /// <summary>
        /// 获取数据提供程序名称
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetProviderName(string key)
        {
            key.CheckNotNull("key");            
            return ConfigurationManager.ConnectionStrings[key].ProviderName;
        }
        #endregion

        #region GetSystemWebSection(获取system.web节点)
        /// <summary>
        /// 获取system.web节点
        /// </summary>
        /// <typeparam name="T">配置节点类型</typeparam>
        /// <param name="sections">节点类型</param>
        /// <returns></returns>
        public static T GetSystemWebSection<T>(SystemWebSections sections) where T : class
        {
            switch (sections)
            {
                case SystemWebSections.Authentication:
                    var authenticationSection = WebConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection;
                    return authenticationSection as T;
                case SystemWebSections.Compilation:
                    var compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
                    return compilationSection as T;
                case SystemWebSections.CustomErrors:
                    var customErrorsSection = WebConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
                    return customErrorsSection as T;
                case SystemWebSections.Globalization:
                    var globalizationSection = WebConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
                    return globalizationSection as T;
                case SystemWebSections.HttpRuntime:
                    var httpRuntimeSection = WebConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                    return httpRuntimeSection as T;
                case SystemWebSections.Identity:
                    var identitySection = WebConfigurationManager.GetSection("system.web/identity") as IdentitySection;
                    return identitySection as T;
                case SystemWebSections.Trace:
                    var traceSection = WebConfigurationManager.GetSection("system.web/trace") as TraceSection;
                    return traceSection as T;
                default:
                    return default(T);
            }
        }
        #endregion
    }
}
