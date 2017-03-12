/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：LoggingConfig
 * 版本号：v1.0.0.0
 * 唯一标识：b215f41b-9723-4c4d-9ee9-ca0a7ac22834
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:24:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:24:08
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

namespace JCE.Core.Configs
{
    /// <summary>
    /// 日志配置信息
    /// </summary>
    public class LoggingConfig
    {
        #region Property(属性)
        /// <summary>
        /// 获取或设置 日志入口配置信息
        /// </summary>
        public LoggingEntryConfig EntryConfig { get; set; }

        /// <summary>
        /// 获取或设置 数据日志配置信息
        /// </summary>
        public DataLoggingConfig DataLoggingConfig { get; set; }

        /// <summary>
        /// 获取或设置 基本日志配置信息
        /// </summary>
        public BasicLoggingConfig BasicLoggingConfig { get; set; }
        #endregion

        /// <summary>
        /// 初始化一个<see cref="LoggingConfig"/>类型的实例
        /// </summary>
        public LoggingConfig()
        {
            EntryConfig=new LoggingEntryConfig();
            DataLoggingConfig=new DataLoggingConfig();
            BasicLoggingConfig=new BasicLoggingConfig();
        }

        /// <summary>
        /// 初始化一个<see cref="LoggingConfig"/>类型的实例
        /// </summary>
        /// <param name="element">日志配置节点</param>
        internal LoggingConfig(LoggingElement element)
        {
            EntryConfig=new LoggingEntryConfig(element.LoggingEntry);
            //DataLoggingConfig = new DataLoggingConfig(element.DataLogging);
            BasicLoggingConfig = new BasicLoggingConfig(element.BasicLogging);
        }
    }
}
