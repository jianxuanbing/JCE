/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：LoggingEntryConfig
 * 版本号：v1.0.0.0
 * 唯一标识：c66894ad-1d25-41a6-9a5f-737bde4fe8d5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:47:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:47:43
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
using JCE.Core.Configs.ConfigFile;
using JCE.Utils.Logging;

namespace JCE.Core.Configs
{
    /// <summary>
    /// 日志记录入口配置
    /// </summary>
    public class LoggingEntryConfig
    {
        /// <summary>
        /// 获取或设置 从入口控制是否允许记录日志
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置 入口允许记录的日志等级
        /// </summary>
        public LogLevel EntryLogLevel { get; set; }

        /// <summary>
        /// 初始化一个<see cref="LoggingEntryConfig"/>类型的实例
        /// </summary>
        public LoggingEntryConfig()
        {
            Enabled = true;
            EntryLogLevel=LogLevel.All;
        }

        /// <summary>
        /// 初始化一个<see cref="LoggingEntryConfig"/>类型的实例
        /// </summary>
        /// <param name="element">日志输入配置节点</param>
        internal LoggingEntryConfig(LoggingEntryElement element)
        {
            Enabled = element.Enabled;
            EntryLogLevel = element.EntryLogLevel;
        }
    }
}
