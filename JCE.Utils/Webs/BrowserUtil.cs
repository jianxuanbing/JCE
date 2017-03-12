/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webs
 * 文件名：BrowserUtil
 * 版本号：v1.0.0.0
 * 唯一标识：16592897-43bf-4242-b9aa-a50395fd7aba
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/22 23:31:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/22 23:31:00
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// 浏览器工具类
    /// </summary>
    public class BrowserUtil
    {
        /// <summary>
        /// 调用Chorem浏览器打开网页
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenChrome(string url)
        {
            try
            {
                //64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Google\Chrome";
                if (IntPtr.Size == 4)
                {
                    //32位注册表路径
                    openKey = @"SOFTWARE\Google\Chrome";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                //谷歌浏览器就用谷歌打开，没找到就用系统默认的浏览器
                //谷歌卸载了，注册表还没有清空，程序会返回一个“系统找不到指定文件”的BUG
                if (appPath != null)
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenIE(url);
                    }
                }
                else
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }

            }
            catch
            {
                //出错调用用户默认设置的浏览器，还不行就调用IE
                OpenDefaultBrowserUrl(url);
            }
        }

        /// <summary>
        /// 调用IE浏览器打开网页
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        // ReSharper disable once InconsistentNaming
        public static void OpenIE(string url)
        {
            try
            {
                Process.Start("iexplore.exe", url);
            }
            catch
            {
                try
                {
                    if (File.Exists(@"C:\Program Files\Internet Explorer\iexplore.exe"))
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Internet Explorer\iexplore.exe",
                            Arguments = url,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        Process.Start(processStartInfo);
                    }
                    else
                    {
                        if (File.Exists(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe"))
                        {
                            ProcessStartInfo processStartInfo = new ProcessStartInfo()
                            {
                                FileName = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe",
                                Arguments = url,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            Process.Start(processStartInfo);
                        }
                        else
                        {
                            OpenDefaultBrowserUrl("http://windows.microsoft.com/zh-cn/internet-explorer/download-ie");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 调用系统默认浏览器打开网页（用户自己设置的默认浏览器）
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenDefaultBrowserUrl(string url)
        {
            try
            {
                //方法一：从注册表读取默认浏览器可执行文件路径
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                if (key != null)
                {
                    string s = key.GetValue("").ToString();
                    //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！
                    //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
                    var lastIndex = s.IndexOf(".exe", StringComparison.Ordinal);
                    if (lastIndex == -1)
                    {
                        lastIndex = s.IndexOf(".EXE", StringComparison.Ordinal);
                    }
                    var path = s.Substring(1, lastIndex + 3);
                    var result = Process.Start(path, url);
                    if (result == null)
                    {
                        //方法二：调用系统默认的浏览器
                        var result1 = Process.Start("explorer.exe", url);
                        if (result1 == null)
                        {
                            //方法三
                            Process.Start(url);
                        }
                    }
                }
                else
                {
                    //方法二：调用系统默认的浏览器
                    var result1 = Process.Start("explorer.exe", url);
                    if (result1 == null)
                    {
                        //方法三
                        Process.Start(url);
                    }
                }
            }
            catch
            {

                OpenIE(url);
            }
        }

        /// <summary>
        /// 调用火狐浏览器打开网页
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenFireFox(string url)
        {
            try
            {
                //64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Mozilla\Mozilla Firefox";
                if (IntPtr.Size == 4)
                {
                    //32位注册表路径
                    openKey = @"SOFTWARE\Mozilla\Mozilla Firefox";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                if (appPath != null)
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenIE(url);
                    }
                }
                else
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }
            }
            catch
            {
                OpenDefaultBrowserUrl(url);
            }

        }
    }
}
