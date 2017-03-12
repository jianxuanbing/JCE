/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：InternalLogger
 * 版本号：v1.0.0.0
 * 唯一标识：29a21151-ee46-4f77-b71d-9e764414f25f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:35:56
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:35:56
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

namespace JCE.Utils.Logging
{
    /// <summary>
    /// 日记记录者，日志记录输入端
    /// </summary>
    internal sealed class InternalLogger : ILogger
    {
        #region Field(字段)
        /// <summary>
        /// 日志实例集合
        /// </summary>
        private readonly ICollection<ILog> _logs;
        #endregion

        #region Property(属性)
        /// <summary>
        /// 获取或设置 是否允许记录日志，如为 false，将完全禁止日志记录
        /// </summary>
        public static bool EntryEnabled { get; set; }

        /// <summary>
        ///  获取或设置 日志级别的入口控制，级别决定是否执行相应级别的日志记录功能
        /// </summary>
        public static LogLevel EntryLogLevel { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 构造函数，默认初始化开启日志
        /// </summary>
        static InternalLogger()
        {
            EntryEnabled = true;
            EntryLogLevel = LogLevel.All;
        }

        /// <summary>
        /// 构造函数，初始化一个<see cref="InternalLogger"/>新实例
        /// </summary>
        /// <param name="type">类型</param>
        public InternalLogger(Type type) : this(type.FullName)
        {

        }
        /// <summary>
        /// 构造函数，初始化一个<see cref="InternalLogger"/>新实例
        /// </summary>
        /// <param name="name">指定名称</param>
        public InternalLogger(string name)
        {
            _logs = LogManager.Adapters.Select(adapter => adapter.GetLogger(name)).ToList();
        }
        #endregion

        #region Trace(输出跟踪日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public void Trace<T>(T message)
        {
            if (!IsEnableFor(LogLevel.Trace))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Trace(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Trace(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Trace))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Trace(format, args);
            }
        }
        #endregion

        #region Debug(输出调试日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public void Debug<T>(T message)
        {
            if (!IsEnableFor(LogLevel.Debug))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Debug(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Debug))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Debug(format, args);
            }
        }
        #endregion

        #region Info(输出信息日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="isData">是否数据日志</param>
        public void Info<T>(T message, bool isData)
        {
            if (!IsEnableFor(LogLevel.Info))
            {
                return;
            }
            var logs = _logs.Where(m => isData ? m.IsDataLogging : !m.IsDataLogging);
            foreach (ILog log in logs)
            {
                log.Info(message, isData);
            }
        }
        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Info(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Info))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Info(format, args);
            }
        }
        #endregion

        #region Warn(输出警告日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public void Warn<T>(T message)
        {
            if (!IsEnableFor(LogLevel.Warn))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Warn(message);
            }
        }
        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Warn(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Warn))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Warn(format, args);
            }
        }
        #endregion

        #region Error(输出错误日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public void Error<T>(T message)
        {
            if (!IsEnableFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public void Error<T>(T message, Exception exception)
        {
            if (!IsEnableFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, Exception exception, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Error))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Error(format, exception, args);
            }
        }
        #endregion

        #region Fatal(输出致命错误日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public void Fatal<T>(T message)
        {
            if (!IsEnableFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(message);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public void Fatal<T>(T message, Exception exception)
        {
            if (!IsEnableFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, Exception exception, params object[] args)
        {
            if (!IsEnableFor(LogLevel.Fatal))
            {
                return;
            }
            foreach (ILog log in _logs)
            {
                log.Fatal(format, exception, args);
            }
        }
        #endregion

        #region IsEnableFor(是否启用指定日志级别)
        /// <summary>
        /// 是否启用指定日志级别
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        private static bool IsEnableFor(LogLevel level)
        {
            return EntryEnabled && level >= EntryLogLevel;
        }
        #endregion

    }
}
