/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：LoggingElement
 * 版本号：v1.0.0.0
 * 唯一标识：ab0bb804-6e94-40c4-8a25-ea2a04c906e7
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:52:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:52:43
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

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// 日志配置节点
    /// </summary>
    internal class LoggingElement:ConfigurationElement
    {
        private const string LoggingEntryKey = "entry";
        private const string DataLoggingKey = "data";
        private const string BasicLoggingKey = "basic";

        /// <summary>
        /// 获取或设置 日志输入配置节点
        /// </summary>
        [ConfigurationProperty(LoggingEntryKey)]
        public virtual LoggingEntryElement LoggingEntry
        {
            get { return (LoggingEntryElement)this[LoggingEntryKey]; }
            set { this[LoggingEntryKey] = value; }
        }

        ///// <summary>
        ///// 获取或设置 数据日志配置节点
        ///// </summary>
        //[ConfigurationProperty(DataLoggingKey)]
        //public virtual DataLoggingElement DataLogging
        //{
        //    get { return (DataLoggingElement)this[DataLoggingKey]; }
        //    set { this[DataLoggingKey] = value; }
        //}

        /// <summary>
        /// 获取或设置 基础日志配置节点
        /// </summary>
        [ConfigurationProperty(BasicLoggingKey)]
        public virtual BasicLoggingElement BasicLogging
        {
            get { return (BasicLoggingElement)this[BasicLoggingKey]; }
            set { this[BasicLoggingKey] = value; }
        }
    }
}
