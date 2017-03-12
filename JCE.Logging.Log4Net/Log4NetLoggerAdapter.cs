/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Logging.Log4Net
 * 文件名：Log4NetLoggerAdapter
 * 版本号：v1.0.0.0
 * 唯一标识：6a232bbb-9355-4276-a28e-58ac2993cb66
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 14:49:16
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 14:49:16
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logging;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using LogManager = log4net.LogManager;

namespace JCE.Logging.Log4Net
{
    /// <summary>
    /// log4net 日志输出适配器
    /// </summary>
    public class Log4NetLoggerAdapter:LoggerAdapterBase
    {
        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Log4NetLoggerAdapter"/>类型的实例
        /// </summary>
        public Log4NetLoggerAdapter()
        {
            const string fileName = "log4net.config";
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(configFile))
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                return;
            }
            RollingFileAppender appender=new RollingFileAppender()
            {
                Name = "root",
                File = "logs\\log_",
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyyMMdd-HH\".log\"",
                StaticLogFileName = false,
                MaxSizeRollBackups = 10,
                Layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c.%M %t %w %n%m%n")
                //Layout = new PatternLayout("[%d [%t] %-5p %c [%x] - %m%n]")
            };
            appender.ClearFilters();
            appender.AddFilter(new LevelMatchFilter() {LevelToMatch = Level.Info});
            BasicConfigurator.Configure(appender);
            appender.ActivateOptions();
        }
        #endregion

        #region CreateLogger(创建日志实例)
        /// <summary>
        /// 创建日志实例，如缓存日志字典存在该名称日志实例，则返回，否则创建新实例并缓存起来
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected override ILog CreateLogger(string name)
        {
            log4net.ILog log = LogManager.GetLogger(name);
            return new Log4NetLog(log);
        }
        #endregion
    }
}
