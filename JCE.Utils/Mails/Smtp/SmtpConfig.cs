/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails.Smtp
 * 文件名：SmtpConfig
 * 版本号：v1.0.0.0
 * 唯一标识：bb86a76e-9890-47b9-89fa-d8b2dd223105
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 22:47:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 22:47:38
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

using System;
using System.IO;
using System.Xml;
using JCE.Utils.Common;

namespace JCE.Utils.Mails.Smtp
{
    /// <summary>
    /// Smtp配置
    /// </summary>
    public class SmtpConfig
    {
        #region Fields(字段)
        /// <summary>
        /// 单例配置
        /// </summary>
        private static SmtpConfig _smtpConfig;
        #endregion

        #region Property(属性)
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigUtil.GetAppSettings("SmtpConfigPath");
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = Sys.GetPhysicalPath("/Config/SmtpSetting.config");
                }
                else
                {
                    if (!Path.IsPathRooted(configPath))
                    {
                        configPath = Sys.GetPhysicalPath(Path.Combine(configPath, "SmtpSetting.config"));
                    }
                    else
                    {
                        configPath = Path.Combine(configPath, "SmtpSetting.config");
                    }
                }
                return configPath;
            }
        }

        /// <summary>
        /// Smtp相关设置
        /// </summary>
        public SmtpSetting SmtpSetting
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.ConfigFile);
                SmtpSetting smtpSetting = new SmtpSetting();
                smtpSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                smtpSetting.Authentication =
                    Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("Authentication").InnerText);
                smtpSetting.Port = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("Port").InnerText);
                smtpSetting.User = doc.DocumentElement.SelectSingleNode("User").InnerText;
                smtpSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;
                smtpSetting.Sender = doc.DocumentElement.SelectSingleNode("Sender").InnerText;

                return smtpSetting;
            }
        }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="SmtpConfig"/>类型的实例
        /// </summary>
        private SmtpConfig() { }
        #endregion

        #region Create(创建SMTP配置)
        /// <summary>
        /// 创建Smtp配置
        /// </summary>
        /// <returns></returns>
        public static SmtpConfig Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new SmtpConfig();
            }
            return _smtpConfig;
        }
        #endregion
    }
}
