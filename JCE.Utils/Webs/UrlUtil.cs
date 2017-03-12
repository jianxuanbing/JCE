/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webs
 * 文件名：UrlUtil
 * 版本号：v1.0.0.0
 * 唯一标识：36d42eae-0331-42fd-89a9-bfb474e1e74f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/22 23:49:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/22 23:49:12
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Extensions;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// Url工具类
    /// </summary>
    public class UrlUtil
    {
        #region Host(获取主机名)
        /// <summary>
        /// 获取主机名，即域名，
        /// 范例：用户输入网址http://www.a.com/b.htm?a=1&amp;b=2，
        /// 返回值为：www.a.com
        /// </summary>
        public static string Host
        {
            get { return HttpContext.Current.Request.Url.Host; }
        }
        #endregion

        #region ResolveUrl(解析相对Url)
        /// <summary>
        /// 解析相对Url
        /// </summary>
        /// <param name="relativeUrl">相对Url</param>
        /// <returns></returns>
        public static string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                return string.Empty;
            }
            relativeUrl = relativeUrl.Replace("\\", "/");
            if (relativeUrl.StartsWith("/"))
            {
                return relativeUrl;
            }
            if (relativeUrl.Contains("://"))
            {
                return relativeUrl;
            }
            return VirtualPathUtility.ToAbsolute(relativeUrl);
        }
        #endregion

        #region HtmlEncode(对html字符串进行编码)
        /// <summary>
        /// 对html字符串进行编码
        /// </summary>
        /// <param name="html">html字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(string html)
        {
            return HttpUtility.HtmlEncode(html);
        }
        #endregion

        #region UrlEncode(对Url进行编码)
        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="isUpper">编码字符是否需要大写，范例，"http://"转成"http%3A%2F%2F"</param>
        /// <returns></returns>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return UrlEncode(url, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否需要大写，范例，"http://"转成"http%3A%2F%2F"</param>
        /// <returns></returns>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            string result = HttpUtility.UrlEncode(url, encoding);
            if (!isUpper)
            {
                return result;
            }
            return GetUpperEncode(result);
        }
        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        /// <param name="encode">需要大写的编码字符串</param>
        /// <returns></returns>
        private static string GetUpperEncode(string encode)
        {
            StringBuilder sb = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                {
                    index = i;
                }
                if (i - index == 1 || i - index == 2)
                {
                    character = character.ToUpper();
                }
                sb.Append(character);
            }
            return sb.ToString();
        }
        #endregion

        #region UrlDecode(对Url进行解码)
        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }
        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="encoding">字符编码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码</param>
        /// <returns></returns>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }
        #endregion

        #region Base64Encrypt(对Url进行Base64编码)
        /// <summary>
        /// 对Url进行Base64编码
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string Base64Encrypt(string url)
        {
            string tempUrl = UrlEncode(url);
            return Convert.ToBase64String(Const.DefaultEncoding.GetBytes(tempUrl));            
        }
        #endregion

        #region Base64Decrypt(对Url进行Base64解码)
        /// <summary>
        /// 对Url进行Base64解码
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string Base64Decrypt(string url)
        {
            if (!Valid.IsBase64(url))
            {
                return url;
            }
            byte[] buffer = Convert.FromBase64String(url);
            string tempUrl = Const.DefaultEncoding.GetString(buffer);
            return UrlDecode(tempUrl);
        }
        #endregion

        #region AddParam(添加Url参数)
        /// <summary>
        /// 添加Url参数
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            string eval = HttpContext.Current.Server.UrlEncode(value);
            if (uri.Query.IsNullOrEmpty())
            {
                return string.Concat(url, "?" + paramName + "=" + eval);
            }
            return string.Concat(url, "&" + paramName + "=" + eval);
        }
        #endregion

        #region UpdateParam(更新Url参数)
        /// <summary>
        /// 更新Url参数
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static string UpdateParam(string url, string paramName, string value)
        {
            string keyWord = paramName + "=";
            int startIndex = url.IndexOf(keyWord, StringComparison.Ordinal) + keyWord.Length;
            int endIndex = url.IndexOf("&", startIndex, StringComparison.Ordinal);
            if (endIndex == -1)
            {
                url = url.Remove(startIndex, url.Length - startIndex);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(startIndex, endIndex - startIndex);
            url = url.Insert(startIndex, value);
            return url;
        }
        #endregion

        #region GetDomain(获取域名信息)
        /// <summary>
        /// 获取域名信息
        /// </summary>
        /// <param name="fromUrl">来源Url</param>
        /// <param name="domain">域名</param>
        /// <param name="subDomain">子域名</param>
        public static void GetDomain(string fromUrl, out string domain, out string subDomain)
        {
            domain = "";
            subDomain = "";
            try
            {
                if (fromUrl.IndexOf("的名片", StringComparison.Ordinal) > -1)
                {
                    subDomain = fromUrl;
                    domain = "名片";
                    return;
                }

                UriBuilder builder = new UriBuilder(fromUrl);
                fromUrl = builder.ToString();

                Uri uri = new Uri(fromUrl);

                if (uri.IsWellFormedOriginalString())
                {
                    if (uri.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        string authority = uri.Authority;
                        string[] ss = authority.Split('.');
                        if (ss.Length == 2)
                        {
                            authority = "www" + authority;
                        }
                        int index = authority.IndexOf('.', 0);
                        domain = authority.Substring(index + 1, authority.Length - index - 1).Replace("comhttp", "com");
                        subDomain = authority.Replace("comhttp", "com");
                        if (ss.Length < 2)
                        {
                            domain = subDomain = "不明路径";
                        }
                    }
                }
                else
                {
                    if (uri.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        domain = subDomain = "不明路径";
                    }
                }
            }
            catch
            {
                domain = subDomain = "不明路径";
            }
        }
        #endregion

        #region ParseUrl(解析Url的参数信息)
        /// <summary>
        /// 解析Url的参数信息
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="baseUrl">基础url</param>
        /// <param name="list">参数集合</param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection list)
        {
            if (url.IsNullOrEmpty())
            {
                throw new ArgumentNullException("url");
            }
            list = new NameValueCollection();
            baseUrl = "";
            int questionMarkIndex = url.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
            {
                return;
            }
            string ps = url.Substring(questionMarkIndex + 1);
            //开始解析参数对
            Regex regex = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = regex.Matches(ps);
            foreach (Match match in mc)
            {
                list.Add(match.Result("$2").ToLower(), match.Result("$3"));
            }
        }
        #endregion
    }
}
