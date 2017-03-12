/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails.Pop3
 * 文件名：Pop3Config
 * 版本号：v1.0.0.0
 * 唯一标识：da1c2f7e-3eee-4d11-9866-b1bc7d099a17
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 23:06:40
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 23:06:40
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

using System;
using System.IO;
using System.Xml;
using JCE.Utils.Common;

namespace JCE.Utils.Mails.Pop3
{
    /// <summary>
    /// Pop3配置
    /// </summary>
    public class Pop3Config
    {
        #region Fields(字段)
        /// <summary>
        /// 单例配置
        /// </summary>
        private static Pop3Config _smtpConfig;
        #endregion

        #region Property(属性)
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigUtil.GetAppSettings("Pop3ConfigPath");
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = Sys.GetPhysicalPath("/Config/Pop3Setting.config");
                }
                else
                {
                    if (!Path.IsPathRooted(configPath))
                    {
                        configPath = Sys.GetPhysicalPath(Path.Combine(configPath, "Pop3Setting.config"));
                    }
                    else
                    {
                        configPath = Path.Combine(configPath, "Pop3Setting.config");
                    }
                }
                return configPath;
            }
        }

        /// <summary>
        /// Smtp相关设置
        /// </summary>
        public Pop3Setting SmtpSetting
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.ConfigFile);
                Pop3Setting smtpSetting = new Pop3Setting();
                smtpSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                smtpSetting.Port = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("Port").InnerText);
                smtpSetting.UseSSL =
                    Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("UseSSL").InnerText);                
                smtpSetting.User = doc.DocumentElement.SelectSingleNode("User").InnerText;
                smtpSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;
                return smtpSetting;
            }
        }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="SmtpConfig"/>类型的实例
        /// </summary>
        private Pop3Config() { }
        #endregion

        #region Create(创建SMTP配置)
        /// <summary>
        /// 创建Smtp配置
        /// </summary>
        /// <returns></returns>
        public static Pop3Config Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new Pop3Config();
            }
            return _smtpConfig;
        }
        #endregion
    }
}
