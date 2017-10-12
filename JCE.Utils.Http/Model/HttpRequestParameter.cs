using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using JCE.Utils.Extensions;
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


        /// <summary>
        /// 添加 Form 表单参数
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public HttpRequestParameter AddParam(string name, object value)
        {
            FormParameters.Add(new FormParameter(name, value));
            return this;
        }


        /// <summary>
        /// 添加 文件 参数
        /// </summary>
        /// <param name="name">参数的名称</param>
        /// <param name="fileStream">文件流，调用方会自动释放</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentType">文件类型</param>
        /// <returns></returns>
        public HttpRequestParameter AddFileParam(string name, Stream fileStream, string fileName, string contentType)
        {
            FileParameters.Add(new FileParameter(name, fileStream, fileName, contentType));
            return this;
        }

        /// <summary>
        /// 设置自定义内容实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="camelCase">是否驼峰式命名</param>
        /// <returns></returns>
        public HttpRequestParameter SetCustomBody<T>(T obj,bool camelCase=false)
        {
            if (obj is string)
            {
                CustomBody = obj as string;
            }
            else
            {
                CustomBody = obj.ToJson(false, camelCase);
            }
            return this;
        }
    }
}
