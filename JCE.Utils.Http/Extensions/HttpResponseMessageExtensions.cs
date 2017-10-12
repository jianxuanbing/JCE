using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpResponseMessage"/> 扩展
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// 获取请求结果字符串
        /// </summary>
        /// <param name="response">响应消息</param>
        /// <returns></returns>
        public static string GetString(this Task<HttpResponseMessage> response)
        {
            var result = response.Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        /// <summary>
        /// 获取请求结果字符串
        /// </summary>
        /// <param name="response">响应消息</param>
        /// <returns></returns>
        public static Task<string> GetStringAsync(this Task<HttpResponseMessage> response)
        {
            var result = response.Result.Content.ReadAsStringAsync();
            return result;
        }

        /// <summary>
        /// 获取请求结果对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="response">响应消息</param>
        /// <returns></returns>
        public static T Get<T>(this Task<HttpResponseMessage> response)
        {
            var result = GetString(response);
            return result.ToObject<T>();
        }
    }
}
