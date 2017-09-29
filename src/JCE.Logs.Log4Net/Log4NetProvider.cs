using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Logs.Formats;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Core;


namespace JCE.Logs.Log4Net
{
    /// <summary>
    /// Log4Net 日志提供程序
    /// </summary>
    public class Log4NetProvider:ILogProvider
    {
        #region Property(属性)
        /// <summary>
        /// Log4Net 日志操作
        /// </summary>
        private readonly log4net.ILog _log;

        /// <summary>
        /// 日志格式化器
        /// </summary>
        private readonly ILogFormat _format;

        /// <summary>
        /// 日志名称
        /// </summary>
        public string LogName => _log.Logger.Name;

        /// <summary>
        /// 调试级别是否启用
        /// </summary>
        public bool IsDebugEnabled => _log.IsDebugEnabled;

        /// <summary>
        /// 跟踪级别是否启用，默认false，log4net 无跟踪级别
        /// </summary>
        public bool IsTraceEnabled => false;

        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Log4NetProvider"/>类型的实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="format">日志格式化器</param>
        public Log4NetProvider(string logName, ILogFormat format = null)
        {
            _log = GetLogger(logName);
            _format = format;
        }

        #endregion

        /// <summary>
        /// 获取Log4Net日志操作
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <returns></returns>
        public static log4net.ILog GetLogger(string logName)
        {
            return log4net.LogManager.GetLogger(logName);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="content">日志内容</param>
        public void WriteLog(LogLevel level, ILogContent content)
        {
            var provider = GetFormatProvider();
            if (provider != null)
            {
                string message=provider.Format("", content, null);
                WriteLog(level, message);
                return;
            }
            throw new NullReferenceException("日志格式化提供程序不可为空");
        }

        /// <summary>
        /// 获取格式化提供程序
        /// </summary>
        /// <returns></returns>
        private FormatProvider GetFormatProvider()
        {
            if (_format == null)
            {
                return null;
            }
            return new FormatProvider(_format);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level">平台日志等级</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        private void WriteLog(JCE.Utils.Logs.Core.LogLevel level,object message)
        {
            switch (level)
            {
                case Utils.Logs.Core.LogLevel.Trace:
                    throw new NotImplementedException();
                case Utils.Logs.Core.LogLevel.Debug:
                    _log.Debug(message);
                    return;
                case Utils.Logs.Core.LogLevel.Information:
                    _log.Info(message);
                    return;
                case Utils.Logs.Core.LogLevel.Warning:
                    _log.Warn(message);
                    return;
                case Utils.Logs.Core.LogLevel.Error:
                    _log.Error(message);
                    return;
                case Utils.Logs.Core.LogLevel.Fatal:
                    _log.Fatal(message);
                    return;
            }
        }
    }
}
