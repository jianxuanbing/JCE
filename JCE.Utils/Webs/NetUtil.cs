/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webs
 * 文件名：NetUtil
 * 版本号：v1.0.0.0
 * 唯一标识：9446dd28-4747-445e-b8c7-a2ffb909f4aa
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/17 9:33:49
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/17 9:33:49
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Extensions;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// 网络操作工具类
    /// </summary>
    public class NetUtil
    {
        #region IP(获取IP地址)
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = GetWebClientIp();
                }
                if (result.IsNullOrEmpty())
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
    }
}
