using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Contexts;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Core;
using JCE.Utils.Logs.Formats;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// 日志管理服务
    /// </summary>
    public class NLogLogManager:LogManagerBase
    {
        /// <summary>
        /// 日志上下文
        /// </summary>
        public ILogContext Context { get; set; }

        /// <summary>
        /// 用户上下文
        /// </summary>
        public IUserContext UserContext { get; set; }

        /// <summary>
        /// 日志格式化器
        /// </summary>
        public ILogFormat Format { get; set; }

        /// <summary>
        /// 初始化一个<see cref="NLogLogManager"/>类型的实例
        /// </summary>
        /// <param name="context">日志上下文</param>
        /// <param name="userContext">用户上下文</param>
        public NLogLogManager(ILogContext context,IUserContext userContext)
        {
            Context = context;
            UserContext = userContext;
            Format=new TextContentFormat();
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        protected override ILog GetLog(string logName, string @class)
        {
            return NLogLog.GetLog(logName, Format, Context, UserContext, @class);
        }
    }
}
