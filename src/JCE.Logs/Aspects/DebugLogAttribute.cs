using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs;

namespace JCE.Logs.Aspects
{
    /// <summary>
    /// 调试日志
    /// </summary>
    public class DebugLogAttribute:LogAttributeBase
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        protected override bool Enabled(ILog log)
        {
            return log.IsDebugEnabled;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        protected override void WriteLog(ILog log)
        {
            log.Debug();
        }
    }
}
