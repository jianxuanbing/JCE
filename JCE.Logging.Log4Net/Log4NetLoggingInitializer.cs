/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Logging.Log4Net
 * 文件名：Log4NetLoggingInitializer
 * 版本号：v1.0.0.0
 * 唯一标识：d3cb0cdc-a0a2-4885-9300-ad3a38d10241
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:21:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:21:58
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
using JCE.Core.Configs;
using JCE.Core.Initialize;
using JCE.Utils.Logging;

namespace JCE.Logging.Log4Net
{
    /// <summary>
    /// log4net 日志初始化器，用于初始化基础日志功能
    /// </summary>
    public class Log4NetLoggingInitializer//:LoggingInitializerBase, IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        public void Initialize(LoggingConfig config)
        {
            LogManager.SetEntryInfo(config.EntryConfig.Enabled,config.EntryConfig.EntryLogLevel);
            LogManager.AddLoggerAdapter(new Log4NetLoggerAdapter());
            //foreach (LoggingAdapterConfig adapterConfig in config.BasicLoggingConfig.AdapterConfigs)
            //{
            //    SetLoggingFromAdapterConfig(adapterConfig);
            //}
        }
    }
}
