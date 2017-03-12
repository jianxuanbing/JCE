/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Winform
 * 文件名：BootUtil
 * 版本号：v1.0.0.0
 * 唯一标识：871b1e3f-5213-4fdb-a555-c8767fa15bcd
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:57:03
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:57:03
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace JCE.Utils.Winform
{
    /// <summary>
    /// 开机启动工具类
    /// </summary>
    public class BootUtil
    {
        /// <summary>
        /// 设置开机启动
        /// </summary>
        /// <param name="name">程序名</param>
        /// <param name="exePath">exe执行文件路径</param>
        public static void SetStart(string name, string exePath)
        {
            RegistryKey currentKey = Registry.CurrentUser;
            RegistryKey run = currentKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                run.SetValue(name, exePath);
            }            
            finally
            {
                currentKey.Close();
            }            
        }

        /// <summary>
        /// 取消开机启动
        /// </summary>
        /// <param name="name">程序名</param>
        public static void CancelStart(string name)
        {
            RegistryKey currentKey = Registry.CurrentUser;
            RegistryKey run = currentKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                run.DeleteValue(name);
            }
            finally
            {
                currentKey.Close();
            }
        }
    }
}
