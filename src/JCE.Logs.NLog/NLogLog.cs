using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Helpers;
using JCE.Utils.Contexts;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Contents;
using JCE.Utils.Logs.Core;
using JCE.Utils.Logs.Formats;
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
        /// <param name="userContext">用户上下文</param>
        /// <param name="class">类名</param>
        internal NLogLog(ILogProvider provider, ILogContext context,IUserContext userContext,string @class) : base(provider, context,userContext)
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

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <returns></returns>
        public static ILog GetLog()
        {
            return GetLog(string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public static ILog GetLog(object instance)
        {
            if (instance == null)
            {
                return GetLog();
            }
            var className = instance.GetType().ToString();
            return GetLog(className, className);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <returns></returns>
        public static ILog GetLog(string logName)
        {
            return GetLog(logName, string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        private static ILog GetLog(string logName, string @class)
        {
            var context = Ioc.Create<ILogContext>();
            var userContext = Ioc.Create<IUserContext>();
            return GetLog(logName, new TextContentFormat(), context, userContext, @class);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="format">日志格式化器</param>
        /// <param name="context">日志上下文</param>
        /// <param name="userContext">用户上下文</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        internal static ILog GetLog(string logName, ILogFormat format, ILogContext context,IUserContext userContext, string @class)
        {
            return new NLogLog(new NLogLogProvider(logName, format), context, userContext, @class);
        }
    }
}
