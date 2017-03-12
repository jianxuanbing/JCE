/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：ILogger
 * 版本号：v1.0.0.0
 * 唯一标识：df01266f-88f6-432b-ad2f-ccd6cc3d8804
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:32:30
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:32:30
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
    /// 日志记录器，定义日志记录行为
    /// </summary>
    public interface ILogger
    {
        #region Method(方法)
        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        void Trace<T>(T message);
        /// <summary>
        /// 写入<see cref="LogLevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Trace(string format, params object[] args);
        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        void Debug<T>(T message);
        /// <summary>
        /// 写入<see cref="LogLevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Debug(string format, params object[] args);
        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="isData">是否数据日志</param>
        void Info<T>(T message, bool isData);

        /// <summary>
        /// 写入<see cref="LogLevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        void Warn<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Warn(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        void Error<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        void Error<T>(T message, Exception exception);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, Exception exception, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        void Fatal<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息，并记录异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        void Fatal<T>(T message, Exception exception);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, Exception exception, params object[] args);
        #endregion
    }
}
