/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Logging.Log4Net
 * 文件名：Log4NetLoggingConfigReseter
 * 版本号：v1.0.0.0
 * 唯一标识：55d5cc6d-d5e7-4640-8b62-0ecb2e0f355a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 16:26:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 16:26:08
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

namespace JCE.Logging.Log4Net
{
    /// <summary>
    /// Log4Net 日志配置信息重置类
    /// </summary>
    public class Log4NetLoggingConfigReseter:ILoggingConfigReseter
    {
        /// <summary>
        /// 日志配置信息重置
        /// </summary>
        /// <param name="config">待重置的日志配置信息</param>
        /// <returns></returns>
        public LoggingConfig Reset(LoggingConfig config)
        {
            if (config.BasicLoggingConfig.AdapterConfigs.Count == 0)
            {
                config.BasicLoggingConfig.AdapterConfigs.Add(new LoggingAdapterConfig()
                {
                    AdapterType = typeof(Log4NetLoggerAdapter)
                });
            }
            return config;
        }
    }
}
