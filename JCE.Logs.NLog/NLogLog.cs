using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Core;
using NLog;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// NLog日志
    /// </summary>
    public class NLogLog:LogBase<LogContent>
    {
        /// <summary>
        /// 类名
        /// </summary>
        private readonly string _class;

        /// <summary>
        /// 初始化一个<see cref="NLogLog"/>类型的实例
        /// </summary>
        /// <param name="provider">日志提供程序</param>
        /// <param name="context">日志上下文</param>
        /// <param name="class">类名</param>
        internal NLogLog(ILogProvider provider, ILogContext context,string @class) : base(provider, context)
        {
            _class = @class;
        }

        /// <summary>
        /// 获取日志内容
        /// </summary>
        /// <returns></returns>
        protected override LogContent GetContent()
        {
            return new LogContent() {Class = _class};
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="content">日志内容</param>
        protected override void Init(LogContent content)
        {
            base.Init(content);            
        }

        ///// <summary>
        ///// 获取日志操作实例
        ///// </summary>
        ///// <returns></returns>
        //public static ILog GetLog()
        //{
        //    return GetLog(string.Empty);
        //}

        ///// <summary>
        ///// 获取日志操作实例
        ///// </summary>
        ///// <param name="instance">实例</param>
        ///// <returns></returns>
        //public static ILog GetLog(object instance)
        //{
        //    if (instance == null)
        //    {
        //        return GetLog();
        //    }
        //    var className = instance.GetType().ToString();
        //    return GetLog(className, className);
        //}

        ///// <summary>
        ///// 获取人日志操作实例
        ///// </summary>
        ///// <param name="logName">日志名称</param>
        ///// <returns></returns>
        //public static ILog GetLog(string logName)
        //{
        //    return GetLog(logName, string.Empty);
        //}

        //public static ILog GetLog(string logName, string @class)
        //{
        //    return GetLog();
        //}

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logger">日志操作</param>
        /// <param name="format">日志格式化器</param>
        /// <param name="context">日志上下文</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        internal static ILog GetLog(Logger logger, ILogFormat format, ILogContext context, string @class)
        {
            return new NLogLog(new NLogLogProvider(logger,format), context,@class);
        }
    }
}
