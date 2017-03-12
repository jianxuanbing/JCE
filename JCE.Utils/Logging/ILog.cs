/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：ILog
 * 版本号：v1.0.0.0
 * 唯一标识：e64ec059-a790-40df-8245-e2d384b81dba
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:35:24
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:35:24
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
    /// 表示日志实例的接口
    /// </summary>
    public interface ILog : ILogger
    {
        #region Property(属性)
        /// <summary>
        /// 获取 是否数据日志对象
        /// </summary>
        bool IsDataLogging { get; }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }
        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }
        #endregion
    }
}
