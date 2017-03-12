/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：LogLevel
 * 版本号：v1.0.0.0
 * 唯一标识：cf91430a-c461-4c67-a248-ead70aecaa5f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:34:34
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:34:34
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

namespace JCE.Utils.Logging
{
    /// <summary>
    /// 日志级别，表示日志输出级别的枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 输出所有级别的日志
        /// </summary>
        All = 0,
        /// <summary>
        /// 输出表示跟踪的日志级别
        /// </summary>
        Trace = 1,
        /// <summary>
        /// 输出表示调试的日志级别
        /// </summary>
        Debug = 2,
        /// <summary>
        /// 输出表示消息的日志级别
        /// </summary>
        Info = 3,
        /// <summary>
        /// 输出表示警告的日志级别
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 输出表示错误的日志级别
        /// </summary>
        Error = 5,
        /// <summary>
        /// 输出表示严重错误的日志级别
        /// </summary>
        Fatal = 6,
        /// <summary>
        /// 关闭所有日志，不输出日志
        /// </summary>
        Off = 7
    }
}
