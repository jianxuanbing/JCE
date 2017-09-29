using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Logs.Formats;
using JCE.Utils.Logs.Abstractions;
using NLog;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// NLog 日志提供程序
    /// </summary>
    public class NLogLogProvider:ILogProvider
    {
        #region Property(属性)
        /// <summary>
        /// NLog 日志操作
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 日志格式化器
        /// </summary>
        private readonly ILogFormat _format;

        /// <summary>
        /// 日志名称
        /// </summary>
        public string LogName => _logger.Name;

        /// <summary>
        /// 调试级别是否启用
        /// </summary>
        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        /// <summary>
        /// 跟踪级别是否启用
        /// </summary>
        public bool IsTraceEnabled => _logger.IsTraceEnabled;

        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="NLogLogProvider"/>类型的实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="format">日志格式化器</param>
        public NLogLogProvider(string logName, ILogFormat format = null)
        {
            _logger = GetLogger(logName);
            _format = format;
        }
        #endregion

        /// <summary>
        /// 获取NLog日志操作
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <returns></returns>
        public static ILogger GetLogger(string logName)
        {
            return LogManager.GetLogger(logName);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="content">日志内容</param>
        public void WriteLog(Utils.Logs.Core.LogLevel level, ILogContent content)
        {
            var provider = GetFormatProvider();
            if (provider == null)
            {
                _logger.Log(ConvertTo(level), content);
                return;
            }
            _logger.Log(ConvertTo(level), provider, content);
        }
                        
        /// <summary>
        /// 获取格式化提供程序
        /// </summary>
        /// <returns></returns>
        private IFormatProvider GetFormatProvider()
        {
            if (_format == null)
            {
                return null;
            }
            return new FormatProvider(_format);
        }

        /// <summary>
        /// 转换日志等级
        /// </summary>
        /// <param name="level">平台日志等级</param>
        /// <returns></returns>
        private LogLevel ConvertTo(JCE.Utils.Logs.Core.LogLevel level)
        {
            switch (level)
            {
                case Utils.Logs.Core.LogLevel.Trace:
                    return LogLevel.Trace;
                case Utils.Logs.Core.LogLevel.Debug:
                    return LogLevel.Debug;
                case Utils.Logs.Core.LogLevel.Information:
                    return LogLevel.Info;
                case Utils.Logs.Core.LogLevel.Warning:
                    return LogLevel.Warn;
                case Utils.Logs.Core.LogLevel.Error:
                    return LogLevel.Error;
                case Utils.Logs.Core.LogLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    return LogLevel.Off;
            }
        }
    }
}
