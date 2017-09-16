using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Contexts;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;
using JCE.Utils.Logs.Abstractions;
using JCE.Utils.Logs.Internal;
using JCE.Utils.Nets;

namespace JCE.Utils.Logs.Core
{
    /// <summary>
    /// 日志上下文
    /// </summary>
    public class LogContext:ILogContext
    {
        #region Property(属性)

        /// <summary>
        /// 日志上下文信息
        /// </summary>
        private LogContextInfo _info;

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId => GetInfo().TraceId;

        /// <summary>
        /// 计时器
        /// </summary>
        public Stopwatch Stopwatch => GetInfo().Stopwatch;

        /// <summary>
        /// IP
        /// </summary>
        public string Ip => GetInfo().Ip;

        /// <summary>
        /// 主机
        /// </summary>
        public string Host => GetInfo().Host;

        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser => GetInfo().Browser;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url => GetInfo().Url;

        /// <summary>
        /// 上下文
        /// </summary>
        public IContext Context { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="LogContext"/>类型的实例
        /// </summary>
        /// <param name="context">上下文</param>
        public LogContext(IContext context)
        {
            Context = context;
        }
        #endregion

        /// <summary>
        /// 获取日志上下文信息
        /// </summary>
        /// <returns></returns>
        private LogContextInfo GetInfo()
        {
            if (_info != null)
            {
                return _info;
            }
            var key = "JCE.Utils.Logs.LogContext";
            _info = Context.Get<LogContextInfo>(key);
            if (_info != null)
            {
                return _info;
            }
            _info = CreateInfo();
            Context.Add(key,_info);
            return _info;
        }

        /// <summary>
        /// 创建日志上下文信息
        /// </summary>
        /// <returns></returns>
        private LogContextInfo CreateInfo()
        {
            var traceId = Context.TraceId;
            if (traceId.IsEmpty())
            {
                traceId = Guid.NewGuid().ToString();
            }
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            return new LogContextInfo()
            {
                TraceId = traceId,
                Stopwatch = stopwatch,
                Ip = WebUtil.Ip,
                Host = WebUtil.Host,
                Browser = WebUtil.Browser,
                Url = WebUtil.Url
            };
        }
    }
}
