/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：DataLoggingElement
 * 版本号：v1.0.0.0
 * 唯一标识：788aa304-99fd-4d4c-b014-2386ff4af7ed
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:48:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:48:05
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
    /// 数据日志配置节点
    /// </summary>
    internal class DataLoggingElement:ConfigurationElement
    {
        private const string EnableKey = "enabled";
        private const string OutLogLevelKey = "level";
        private const string TypeKey = "type";

        /// <summary>
        /// 获取或设置 是否允许数据日志输出
        /// </summary>
        [ConfigurationProperty(EnableKey,DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool) this[EnableKey]; }
            set { this[EnableKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据日志输出级别
        /// </summary>
        [ConfigurationProperty(OutLogLevelKey, DefaultValue = LogLevel.All)]
        public virtual LogLevel OutLogLevel
        {
            get { return (LogLevel)this[OutLogLevelKey]; }
            set { this[OutLogLevelKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据日志输出适配器类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, DefaultValue = "JCE.Core.Logging.DatabaseLoggerAdapter, JCE.Core")]
        public virtual string AdapterTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }
    }
}
