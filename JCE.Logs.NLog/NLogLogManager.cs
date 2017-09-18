using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Formats;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// 日志管理服务
    /// </summary>
    public class NLogLogManager:ILogManager
    {
        /// <summary>
        /// 日志上下文
        /// </summary>
        public ILogContext Context { get; set; }

        /// <summary>
        /// 日志格式化器
        /// </summary>
        public ILogFormat Format { get; set; }

        /// <summary>
        /// 初始化一个<see cref="NLogLogManager"/>类型的实例
        /// </summary>
        /// <param name="context">日志伤心爱问</param>
        public NLogLogManager(ILogContext context)
        {
            Context = context;
            Format=new TextContentFormat();
        }

        public ILog GetLog()
        {
            throw new NotImplementedException();
        }

        public ILog GetLog(object instance)
        {
            throw new NotImplementedException();
        }

        public ILog GetLog(string logName)
        {
            throw new NotImplementedException();
        }

        private ILog GetLog(string logName, string @class)
        {
            throw new NotImplementedException();
            //return NLogLog
        }
    }
}
