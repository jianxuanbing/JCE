using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JCE.Utils.Http.Model;

namespace JCE.Utils.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpRequestParameter"/> 扩展
    /// </summary>
    public static class HttpRequestParameterExtensions
    {
        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="request">Http请求参数</param>
        /// <param name="client">Http请求客户端</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpRequestParameter request, HttpClient client = null)
        {
            return SendReq(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None, client);
        }

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="request">Http请求参数</param>
        /// <param name="completionOption">Http完成操作</param>
        /// <param name="client">Http请求客户端</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpRequestParameter request,
            HttpCompletionOption completionOption, HttpClient client = null)
        {
            return SendReq(request, completionOption, CancellationToken.None, client);
        }

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="request">Http请求参数</param>
        /// <param name="completionOption">Http完成操作</param>
        /// <param name="cancellationToken">取消操作通知</param>
        /// <param name="client">Http请求客户端</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpRequestParameter request,
            HttpCompletionOption completionOption, CancellationToken cancellationToken, HttpClient client = null)
        {
            return (client ?? GetDefaultClient()).SendReq(request, completionOption, cancellationToken);
        }

        /// <summary>
        /// Http请求客户端
        /// </summary>
        private static HttpClient _client = null;

        /// <summary>
        /// 获取默认Http请求客户端
        /// </summary>
        /// <returns></returns>
        private static HttpClient GetDefaultClient()
        {
            if (_client != null)
            {
                return _client;
            }
            return _client = new HttpClient();
        }
    }
}
