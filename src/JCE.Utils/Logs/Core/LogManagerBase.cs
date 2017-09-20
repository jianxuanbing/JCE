using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Utils.Logs.Core
{
    /// <summary>
    /// 日志管理服务基类
    /// </summary>
    public abstract class LogManagerBase:ILogManager
    {
        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <returns></returns>
        public ILog GetLog()
        {
            return GetLog(string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public ILog GetLog(object instance)
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
        public ILog GetLog(string logName)
        {
            return GetLog(logName, string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        protected abstract ILog GetLog(string logName, string @class);
    }
}
