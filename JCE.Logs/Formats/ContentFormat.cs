using System;
using System.Text;
using JCE.Logs.Contents;
using JCE.Utils.Logs.Abstractions;

namespace JCE.Logs.Formats
{
    /// <summary>
    /// 内容格式化器
    /// </summary>
    public class ContentFormat: ILogFormat
    {
        /// <summary>
        /// 行号
        /// </summary>
        private int _line;

        /// <summary>
        /// 初始化一个<see cref="ContentFormat"/>类型的实例
        /// </summary>
        public ContentFormat()
        {
            InitLine();
        }

        /// <summary>
        /// 初始化行号
        /// </summary>
        private void InitLine()
        {
            _line = 1;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <returns></returns>
        public string Format(ILogContent logContent)
        {            
            if (!(logContent is LogContent))
            {
                return string.Empty;
            }
            return Format((LogContent) logContent);
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <returns></returns>
        protected virtual string Format(LogContent content)
        {
            StringBuilder result=new StringBuilder();
            Line1(result,content);
            Line2(result, content);
            Line3(result, content);
            Line4(result, content);
            Line5(result, content);
            Line6(result, content);
            Line7(result, content);
            Line8(result, content);
            Line9(result, content);
            Line10(result, content);
            Line11(result, content);
            Line12(result, content);
            Line13(result, content);
            Line14(result, content);
            Finish(result);            
            return result.ToString();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        /// <param name="action">操作</param>
        protected void AppendLine(StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action)
        {
            Append(result,content,action);
            result.AppendLine();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        /// <param name="action">操作</param>
        protected void Append(StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action)
        {
            result.AppendFormat("{0}. ", _line++);
            action(result, content);
        }

        /// <summary>
        /// 添加日志内容
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="caption">标题</param>
        /// <param name="value">值</param>
        protected void Append(StringBuilder result, string caption, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            result.AppendFormat("{0}: {1}   ", caption, value);
        }

        /// <summary>
        /// 第1行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line1(StringBuilder result, LogContent content)
        {
            AppendLine(result,content, (r, c) =>
            {
                r.AppendFormat("{0}: {1} >> ", c.Level, c.LogName);
                r.AppendFormat("{0}: {1}    ", LogResource.TraceId, c.TraceId);
                r.AppendFormat("{0}: {1}    ", LogResource.OperationTime, c.OperationTime);
                if (string.IsNullOrWhiteSpace(c.Duration))
                {
                    return;                    
                }
                r.AppendFormat("{0}: {1}    ", LogResource.Duration, c.Duration);
            });
        }

        /// <summary>
        /// 第2行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line2(StringBuilder result, LogContent content)
        {
            AppendLine(result,content, (r, c) =>
            {
                Append(r,"IP",c.Ip);
                Append(r,LogResource.Host,c.Host);
                Append(r,LogResource.ThreadId,c.ThreadId);
            });
        }

        /// <summary>
        /// 第3行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line3(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.Browser))
            {
                return;                    
            }
            AppendLine(result,content,(r,c)=>Append(r,LogResource.Browser,c.Browser));
        }

        /// <summary>
        /// 第4行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line4(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.Url))
            {
                return;
            }
            AppendLine(result, content, (r, c) => r.Append("Url: " + c.Url));
        }

        /// <summary>
        /// 第5行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line5(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.UserId) && string.IsNullOrWhiteSpace(content.Operator)&&string.IsNullOrWhiteSpace(content.Role))
            {
                return;
            }
            AppendLine(result,content, (r, c) =>
            {
                Append(r,LogResource.UserId,c.UserId);
                Append(r,LogResource.Operator,c.Operator);
                Append(r,LogResource.Role,c.Role);
            });
        }

        /// <summary>
        /// 第6行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line6(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.BussinessId) && string.IsNullOrWhiteSpace(content.Tenant) &&
                string.IsNullOrWhiteSpace(content.Application) && string.IsNullOrWhiteSpace(content.Module))
            {
                return;
            }
            AppendLine(result,content, (r, c) =>
            {
                Append(r,LogResource.BusinessId,c.BussinessId);
                Append(r,LogResource.Tenant,c.Tenant);
                Append(r,LogResource.Application,c.Application);
                Append(r,LogResource.Module,c.Module);
            });
        }

        /// <summary>
        /// 第7行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line7(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.Class) && string.IsNullOrWhiteSpace(content.Method))
            {
                return;
            }
            AppendLine(result,content, (r, c) =>
            {
                Append(r,LogResource.Class,c.Class);
                Append(r,LogResource.Method,c.Method);
            });
        }

        /// <summary>
        /// 第8行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line8(StringBuilder result, LogContent content)
        {
            if (content.Params.Length == 0)
            {
                return;
            }
            Append(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.Params}:");
                r.Append(c.Params);
            });
        }

        /// <summary>
        /// 第9行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line9(StringBuilder result, LogContent content)
        {
            if (string.IsNullOrWhiteSpace(content.Caption))
            {
                return;                    
            }
            AppendLine(result,content, (r, c) =>
            {
                r.AppendFormat("{0}: {1}", LogResource.Caption, c.Caption);
            });
        }

        /// <summary>
        /// 第10行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line10(StringBuilder result, LogContent content)
        {
            if (content.Content.Length == 0)
            {
                return;
            }
            Append(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.Content}");
                r.Append(c.Content);
            });
        }

        /// <summary>
        /// 第11行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line11(StringBuilder result, LogContent content)
        {
            if (content.Sql.Length == 0)
            {
                return;
            }
            Append(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.Sql}:");
                r.Append(c.Sql);
            });
        }

        /// <summary>
        /// 第12行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line12(StringBuilder result, LogContent content)
        {
            if (content.SqlParams.Length == 0)
            {
                return;
            }
            Append(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.SqlParams}:");
                r.Append(c.SqlParams);
            });
        }

        /// <summary>
        /// 第13行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line13(StringBuilder result, LogContent content)
        {
            if (content.Exception == null)
            {
                return;
            }
            AppendLine(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.Exception}: {GetErrorCode(content.ErrorCode)}");
                r.Append($"   {c.Exception.Message}");
            });
        }

        /// <summary>
        /// 获取错误码
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns></returns>
        private string GetErrorCode(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                return string.Empty;
            }
            return $"-- {LogResource.ErrorCode}: {errorCode}";
        }

        /// <summary>
        /// 第14行
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="content">日志内容</param>
        protected void Line14(StringBuilder result, LogContent content)
        {
            if (content.Exception == null)
            {
                return;
            }
            AppendLine(result,content, (r, c) =>
            {
                r.AppendLine($"{LogResource.StackTrace}:");
                r.Append(c.Exception.StackTrace);
            });
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="result">拼接器</param>
        protected void Finish(StringBuilder result)
        {
            for (int i = 0; i < 125; i++)
            {
                result.Append("-");
            }
            InitLine();
        }
    }
}
