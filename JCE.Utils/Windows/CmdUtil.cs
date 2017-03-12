/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Windows
 * 文件名：CmdUtil
 * 版本号：v1.0.0.0
 * 唯一标识：b734cf16-973e-4ae4-9c16-e43e8f8bf362
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 20:35:32
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 20:35:32
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

namespace JCE.Utils.Windows
{
    /// <summary>
    /// Dos cmd命令执行工具类
    /// </summary>
    public class CmdUtil
    {
        #region Run(运行Dos命令)
        /// <summary>
        /// 运行Dos命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static string Run(string command)
        {
            string str = "";
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            using (Process process = Process.Start(startInfo))
            {
                if (process != null)
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        str = reader.ReadToEnd();
                    }
                    process.WaitForExit();
                }
            }
            return str.Trim();
        }
        /// <summary>
        /// 运行进程
        /// </summary>
        /// <param name="exe">执行程序路径</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        public static string Run(string exe, string command)
        {
            string result="";
            using (Process process=new Process())
            {
                process.StartInfo.FileName = exe;
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;//输出信息重定向
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();//启动线程
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                using (StreamReader reader=process.StandardOutput)
                {
                    result=reader.ReadToEnd();
                }
                process.WaitForExit();//等待进程结束
            }
            return result.Trim();
        }
        #endregion
    }
}
