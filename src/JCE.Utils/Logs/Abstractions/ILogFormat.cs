using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Logs.Abstractions
{
    /// <summary>
    /// 日志格式化器
    /// </summary>
    public interface ILogFormat
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <returns></returns>
        string Format(ILogContent content);
    }
}
