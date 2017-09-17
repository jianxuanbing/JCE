using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Utils.Logs.Extensions
{
    /// <summary>
    /// 日志操作 扩展
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <param name="value">值</param>
        /// <param name="args">变量值</param>
        /// <returns></returns>
        public static ILog Content(this ILog log, string value, params object[] args)
        {
            return log.Set<ILogContent>(content => content.Content(value, args));
        }

        /// <summary>
        /// 设置内容并换行
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <param name="value">值</param>
        /// <param name="args">变量值</param>
        /// <returns></returns>
        public static ILog ContentLine(this ILog log, string value, params object[] args)
        {
            return log.Set<ILogContent>(content => content.ContentLine(value, args));
        }
    }
}
