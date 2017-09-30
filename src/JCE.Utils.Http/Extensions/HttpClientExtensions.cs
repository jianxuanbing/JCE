using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JCE.Utils.Http.Model;

namespace JCE.Utils.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpClient"/> 扩展
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 换行符
        /// </summary>
        private const string _lineBreak = "\r\n";

        /// <summary>
        /// 编码格式
        /// </summary>
        public static Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="client">Http客户端</param>
        /// <param name="request">Http请求参数</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpClient client, HttpRequestParameter request)
        {
            return SendReq(client, request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
        }

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="client">Http客户端</param>
        /// <param name="request">Http请求参数</param>
        /// <param name="completionOption">Http完成操作</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpClient client, HttpRequestParameter request,
            HttpCompletionOption completionOption)
        {
            return SendReq(client, request, completionOption, CancellationToken.None);
        }

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="client">Http客户端</param>
        /// <param name="request">Http请求参数</param>
        /// <param name="completionOption">Http完成操作</param>
        /// <param name="cancellationToken">取消操作通知</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendReq(this HttpClient client, HttpRequestParameter request,HttpCompletionOption completionOption,CancellationToken cancellationToken)
        {
            var reqMsg = ConfigureReqMsg(request);
            if (request.TimeOut > 0)
            {
                client.Timeout = TimeSpan.FromMilliseconds(request.TimeOut);
            }
            return client.SendAsync(reqMsg, completionOption, cancellationToken);
        }

        /// <summary>
        /// 配置请求消息
        /// </summary>
        /// <param name="request">Http请求参数</param>
        /// <returns></returns>
        public static HttpRequestMessage ConfigureReqMsg(HttpRequestParameter request)
        {
            var reqMsg=new HttpRequestMessage();
            reqMsg.RequestUri = string.IsNullOrWhiteSpace(request.AddressUrl)
                ? request.Uri
                : new Uri(request.AddressUrl);
            reqMsg.Method = new HttpMethod(request.Method.ToString().ToUpper());
            ConfigReqContent(reqMsg,request);//配置内容
            return reqMsg;
        }

        /// <summary>
        /// 配置使用的Content
        /// </summary>
        /// <param name="reqMsg">请求消息</param>
        /// <param name="request">Http请求参数</param>
        private static void ConfigReqContent(HttpRequestMessage reqMsg, HttpRequestParameter request)
        {
            if (request.Method == Enums.HttpMethod.Get)
            {
                return;
            }
            string boundary = null;
            if (request.HasFile)
            {
                boundary = GetBoundary();
                var memory=new MemoryStream();
                WriteMultipartFormData(memory,request,boundary);
                memory.Seek(0, SeekOrigin.Begin);//设置指针到起点
                reqMsg.Content=new StreamContent(memory);
            }
            else
            {
                string data = GetNormalFormData(request);
                reqMsg.Content=new StringContent(data);
                reqMsg.Content.Headers.ContentType=new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            }
            request.RequestSettings?.Invoke(reqMsg);// 位置不能变，防止外部修改 Content-Type
            if (request.HasFile)
            {
                reqMsg.Content.Headers.Remove("Content-Type");
                reqMsg.Content.Headers.TryAddWithoutValidation("Content-Type",
                    $"multipart/form-data;boundary={boundary}");
            }
        }

        #region 文件上传请求
        /// <summary>
        /// 写入 Form 的内容值【非文件参数+文件头+文件参数(内部完成)+请求结束符】
        /// </summary>
        /// <param name="memory">流</param>
        /// <param name="request">Http请求参数</param>
        /// <param name="boundary">请求分隔符</param>
        private static void WriteMultipartFormData(Stream memory, HttpRequestParameter request, string boundary)
        {
            foreach (var param in request.FormParameters)
            {
                WriteStringTo(memory,GetMultipartFormData(param,boundary));
            }
            foreach (var param in request.FileParameters)
            {
                // 文件头
                WriteStringTo(memory,GetMultipartFileHeader(param,boundary));
                // 文件内容
                param.Writer(memory);
                // 文件结尾
                WriteStringTo(memory,_lineBreak);
            }
            // 写入整个请求的底部信息
            WriteStringTo(memory,GetMultipartFooter(boundary));
        }

        /// <summary>
        /// 写入 Form 的内容值（文件头）
        /// </summary>
        /// <param name="file">文件参数</param>
        /// <param name="boundary">请求分割界限</param>
        /// <returns></returns>
        private static string GetMultipartFileHeader(FileParameter file, string boundary)
        {
            var conType = file.ContentType ?? "application/octet-stream";
            return $"--{boundary}{_lineBreak}Content-Disposition: form-data; name=\"{file.Name}\"; filename=\"{file.FileName}\"{_lineBreak}Content-Type: {conType}{_lineBreak}{_lineBreak}";
        }

        /// <summary>
        /// 写入 Form 的内容值（非文件参数）
        /// </summary>
        /// <param name="param">Form 表单参数</param>
        /// <param name="boundary">请求分割界限</param>
        /// <returns></returns>
        private static string GetMultipartFormData(FormParameter param, string boundary)
        {
            return
                $"--{boundary}{_lineBreak}Content-Disposition: form-data; name=\"{param.Name}\"{_lineBreak}{_lineBreak}{param.Value}{_lineBreak}";
        }

        /// <summary>
        /// 写入 Form 的内容值（请求结束符）
        /// </summary>
        /// <param name="boundary">请求分割界限</param>
        /// <returns></returns>
        private static string GetMultipartFooter(string boundary)
        {
            return $"--{boundary}--{_lineBreak}";
        }
        #endregion

        #region 非文件上传请求
        /// <summary>
        /// 获取请求的内容信息（非文件上传请求）
        /// eg:正常 get / post 请求
        /// </summary>
        /// <param name="request">Http请求参数</param>
        /// <returns></returns>
        private static string GetNormalFormData(HttpRequestParameter request)
        {
            var formString = new StringBuilder();
            foreach (var parm in request.FormParameters)
            {
                if (formString.Length > 1)
                {
                    formString.Append("&");
                }
                formString.AppendFormat(parm.ToString());
            }
            if (!string.IsNullOrWhiteSpace(request.CustomBody))
            {
                if (formString.Length > 1)
                {
                    formString.Append("&");
                }
                formString.Append(request.CustomBody);
            }
            return formString.ToString();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 写入数据方法，将数据写入WebRequest
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="toWrite">需要写入数据的内容</param>
        private static void WriteStringTo(Stream stream, string toWrite)
        {
            var bytes = Encoding.GetBytes(toWrite);
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 创建 请求 分割界限
        /// </summary>
        /// <returns></returns>
        private static string GetBoundary()
        {
            string pattern = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder boundaryBuilder = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                var index = rnd.Next(pattern.Length);
                boundaryBuilder.Append(pattern[index]);
            }
            return $"-------{boundaryBuilder}";
        }
        #endregion

    }
}
