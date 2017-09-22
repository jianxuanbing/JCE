using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Utils.Logs.Extensions
{
    /// <summary>
    /// 日志内容 扩展
    /// </summary>
    public static class LogContentExtensions
    {
        /// <summary>
        /// 追加内容
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="result">拼接字符串</param>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public static void Append(this ILogContent content, StringBuilder result, string value, params object[] args)
        {
            if (value.IsEmpty())
            {
                return;
            }
            result.Append("   ");
            if (args == null || args.Length == 0)
            {
                result.Append(value);
                return;
            }
            result.AppendFormat(value, args);
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="result">拼接字符串</param>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public static void AppendLine(this ILogContent content, StringBuilder result, string value, params object[] args)
        {
            content.Append(result,value,args);
            result.AppendLine();
        }

        /// <summary>
        /// 设置内容并换行
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="value">值</param>
        /// <param name="args">变量值</param>
        public static void Content(this ILogContent content, string value, params object[] args)
        {
            content.AppendLine(content.Content,value,args);
        }
    }
}
