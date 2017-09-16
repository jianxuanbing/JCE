using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Extensions;

namespace JCE.Utils.Nets
{
    /// <summary>
    /// Web工具
    /// </summary>
    public static class WebUtil
    {
        #region IP(获取IP地址)
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string Ip
        {
            get
            {
                var list = new[] {"127.0.0.1", "::1"};
                var result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = GetWebClientIp();
                }
                if (result.IsEmpty() || list.Contains(result))
                {
                    result = GetLanIp();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端的IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取Web远程IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebRemoteIp()
        {            
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                   HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        /// <returns></returns>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }
        #endregion

        #region Host(获取主机名)
        /// <summary>
        /// 获取主机名
        /// </summary>
        public static string Host
        {
            get { return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName(); }
        }
        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal)
            {
                return string.Empty;
            }
            var ip = GetWebRemoteIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
            {
                result = Dns.GetHostName();
            }
            return result;
        }
        #endregion

        #region Browser(获取浏览器信息)
        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }
        #endregion

        #region Url(获取请求地址)
        /// <summary>
        /// 获取请求地址
        /// </summary>
        public static string Url
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.Request.Url.ToString();
                }
                return result;
            }
        }
        #endregion

        #region GetHostAddresses(获取指定url地址IP信息)
        /// <summary>
        /// 获取指定url地址IP信息
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static IPAddress[] GetHostAddresses(string url)
        {
            if (url.Contains("http"))
            {
                url = url.Replace("http://", "").Replace("https://", "");
            }
            return Dns.GetHostAddresses(url);
        }
        #endregion

        
    }
}
