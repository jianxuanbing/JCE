/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Logging.Log4Net
 * 文件名：Log4NetLog
 * 版本号：v1.0.0.0
 * 唯一标识：9c3ad85f-f3f0-412b-9367-7ad5881eafc3
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 14:26:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 14:26:50
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
using JCE.Utils.Extensions;
using JCE.Utils.Logging;
using log4net.Core;
using ILogger = log4net.Core.ILogger;

namespace JCE.Logging.Log4Net
{
    /// <summary>
    /// log4net 日志输出者适配类
    /// </summary>
    internal class Log4NetLog:LogBase
    {
        #region Field(字段)
        /// <summary>
        /// 声明类型
        /// </summary>
        private static readonly Type DeclaringType = typeof(Log4NetLog);
        /// <summary>
        /// log4net日志记录器
        /// </summary>
        private readonly ILogger _logger;
        #endregion

        #region Property(属性)
        /// <summary>
        /// 获取 是否数据日志对象
        /// </summary>
        public override bool IsDataLogging
        {
            get { return false; }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public override bool IsTraceEnabled
        {
            get
            {
                return _logger.IsEnabledFor(Level.Trace);
            }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsEnabledFor(Level.Debug); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return _logger.IsEnabledFor(Level.Info); }            
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return _logger.IsEnabledFor(Level.Warn); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return _logger.IsEnabledFor(Level.Error); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return _logger.IsEnabledFor(Level.Fatal); }
        }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Log4NetLog"/>类型的实例
        /// </summary>
        /// <param name="wrapper">日志记录器包装</param>
        public Log4NetLog(ILoggerWrapper wrapper)
        {
            _logger = wrapper.Logger;
        }
        #endregion

        #region Write(获取日志输出处理委托实例)
        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        /// <param name="isData">是否数据日志</param>
        protected override void Write(LogLevel level, object message, Exception exception, bool isData = false)
        {
            if (isData)
            {
                return;
            }
            Level log4NetLevel = GetLevel(level);
            if (message.GetType() != typeof(string))
            {
                message = message.ToJson();
            }
            _logger.Log(DeclaringType, log4NetLevel, message, exception);
        }
        #endregion

        #region GetLevel(获取日志输出级别)
        /// <summary>
        /// 获取日志输出级别
        /// </summary>
        /// <param name="level">日志输出级别枚举</param>
        /// <returns>获取日志输出级别</returns>
        private static Level GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                case LogLevel.Off:
                    return Level.Off;
                default:
                    return Level.Off;
            }
        }
        #endregion
    }
}
