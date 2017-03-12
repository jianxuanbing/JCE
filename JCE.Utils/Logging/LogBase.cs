/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：LogBase
 * 版本号：v1.0.0.0
 * 唯一标识：84f208fa-19b4-4381-9df4-feef44a63e6c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:36:21
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:36:21
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
    /// 日志输出者适配器基类，用于定义日志输出的处理业务
    /// </summary>
    public abstract class LogBase : ILog
    {
        #region Property(属性)
        /// <summary>
        /// 获取 是否数据日志对象
        /// </summary>
        public abstract bool IsDataLogging { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public abstract bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public abstract bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public abstract bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public abstract bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public abstract bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public abstract bool IsFatalEnabled { get; }
        #endregion

        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        /// <param name="isData">是否数据日志</param>
        protected abstract void Write(LogLevel level, object message, Exception exception, bool isData = false);

        #region Trace(输出跟踪日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public virtual void Trace<T>(T message)
        {
            if (IsTraceEnabled)
            {
                Write(LogLevel.Trace, message, null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Trace(string format, params object[] args)
        {
            if (IsTraceEnabled)
            {
                Write(LogLevel.Trace, string.Format(format, args), null);
            }
        }
        #endregion

        #region Debug(输出调试日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public virtual void Debug<T>(T message)
        {
            if (IsDebugEnabled)
            {
                Write(LogLevel.Debug, message, null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Debug(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                Write(LogLevel.Debug, string.Format(format, args), null);
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
        public virtual void Info<T>(T message, bool isData)
        {
            if (IsInfoEnabled)
            {
                Write(LogLevel.Info, message, null, isData);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Info(string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                Write(LogLevel.Info, string.Format(format, args), null);
            }
        }
        #endregion

        #region Warn(输出警告日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public virtual void Warn<T>(T message)
        {
            if (IsWarnEnabled)
            {
                Write(LogLevel.Warn, message, null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Warn(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                Write(LogLevel.Warn, string.Format(format, args), null);
            }
        }
        #endregion

        #region Error(输出错误日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public virtual void Error<T>(T message)
        {
            if (IsErrorEnabled)
            {
                Write(LogLevel.Error, message, null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Error(string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                Write(LogLevel.Error, string.Format(format, args), null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public virtual void Error<T>(T message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                Write(LogLevel.Error, message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public virtual void Error(string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                Write(LogLevel.Error, string.Format(format, args), exception);
            }
        }
        #endregion

        #region Fatal(输出致命错误日志)
        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        public virtual void Fatal<T>(T message)
        {
            if (IsFatalEnabled)
            {
                Write(LogLevel.Fatal, message, null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public virtual void Fatal(string format, params object[] args)
        {
            if (IsFatalEnabled)
            {
                Write(LogLevel.Fatal, string.Format(format, args), null);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        public virtual void Fatal<T>(T message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                Write(LogLevel.Fatal, message, exception);
            }
        }

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        public virtual void Fatal(string format, Exception exception, params object[] args)
        {
            if (IsFatalEnabled)
            {
                Write(LogLevel.Fatal, string.Format(format, args), exception);
            }
        }
        #endregion

    }
}
