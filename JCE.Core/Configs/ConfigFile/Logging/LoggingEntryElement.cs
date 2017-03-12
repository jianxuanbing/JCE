/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：LoggingEntryElement
 * 版本号：v1.0.0.0
 * 唯一标识：77cc6f0c-ab31-467d-a72b-71045c062294
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:52:54
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:52:54
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logging;

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// 日志输入配置节点
    /// </summary>
    internal class LoggingEntryElement:ConfigurationElement
    {
        private const string EnabledKey = "enabled";
        private const string EntryLogLevelKey = "level";

        /// <summary>
        /// 获取或设置 是否允许日志输入
        /// </summary>
        [ConfigurationProperty(EnabledKey,DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool) this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 日志输入级别
        /// </summary>
        [ConfigurationProperty(EntryLogLevelKey, DefaultValue = LogLevel.All)]
        public virtual LogLevel EntryLogLevel
        {
            get { return (LogLevel)this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }
    }
}
