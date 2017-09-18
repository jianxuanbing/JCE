using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HttpMethod = JCE.Utils.Http.Enums.HttpMethod;

namespace JCE.Utils.Http.Model
{
    /// <summary>
    /// Http 请求参数
    /// </summary>
    public class HttpRequestParameter
    {

        /// <summary>
        /// 请求地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 请求地址，如果此值设置，则忽略<see cref="Uri"/>值
        /// </summary>
        public string AddressUrl { get; set; }

        /// <summary>
        /// Http请求方式
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.Get;

        /// <summary>
        /// 请求消息 设置方法，如果当前的设置不能满足需求，可以通过这里再次设置
        /// </summary>
        public Action<HttpRequestMessage> RequestSettings { get; set; }

        /// <summary>
        /// 文件参数列表
        /// </summary>
        public List<FileParameter> FileParameters { get; set; }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        public bool HasFile => FileParameters.Any();

        /// <summary>
        /// Form 表单参数，非文件参数列表
        /// </summary>
        public List<FormParameter> FormParameters { get; set; }

        /// <summary>
        /// 自定义内容实体。
        /// eg:当上传文件时，无法自定义内容
        /// </summary>
        public string CustomBody { get; set; }

        /// <summary>
        /// 超时时间（毫秒）
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 初始化一个<see cref="HttpRequestParameter"/>类型的实例
        /// </summary>
        public HttpRequestParameter()
        {
            FormParameters=new List<FormParameter>();
            FileParameters=new List<FileParameter>();
        }
    }
}
