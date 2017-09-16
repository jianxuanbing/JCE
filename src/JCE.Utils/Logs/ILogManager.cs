using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Utils.Logs
{
    /// <summary>
    /// 日志管理服务
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <returns></returns>
        ILog GetLog();

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        ILog GetLog(object instance);

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <returns></returns>
        ILog GetLog(string logName);
    }
}
