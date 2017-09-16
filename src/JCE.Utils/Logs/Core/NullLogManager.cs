using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Utils.Logs.Core
{
    /// <summary>
    /// 空日志管理服务
    /// </summary>
    public class NullLogManager:ILogManager
    {
        #region Property(属性)
        /// <summary>
        /// 日志管理服务实例
        /// </summary>
        public static readonly ILogManager Instance=new NullLogManager();
        #endregion

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <returns></returns>
        public ILog GetLog()
        {
            return NullLog.Instance;
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public ILog GetLog(object instance)
        {
            return NullLog.Instance;
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <returns></returns>
        public ILog GetLog(string logName)
        {
            return NullLog.Instance;
        }
    }
}
