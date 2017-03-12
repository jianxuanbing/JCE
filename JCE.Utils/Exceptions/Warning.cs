/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Exceptions
 * 文件名：Warning
 * 版本号：v1.0.0.0
 * 唯一标识：4aa86a42-0639-4da2-9374-cd952b62f7ac
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/12 18:48:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/12 18:48:36
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Logging;

namespace JCE.Utils.Exceptions
{
    /// <summary>
    /// 应用程序异常
    /// </summary>
    public class Warning:Exception
    {
        #region Field(字段)
        /// <summary>
        /// 错误消息
        /// </summary>
        private readonly string _message;

        #endregion

        #region Property(属性)
        /// <summary>
        /// 错误消息
        /// </summary>
        public override string Message
        {
            get
            {
                if (Data.Count == 0)
                {
                    return _message;
                }
                return _message + Environment.NewLine + GetData(this);
            }
        }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public override string StackTrace
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.StackTrace))
                {
                    return base.StackTrace;
                }
                return base.InnerException == null ? string.Empty : base.InnerException.StackTrace;
            }
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; set; }


        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        public Warning(string message) : this(message, "")
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public Warning(string message, string code) : this(message, code, LogLevel.Warn)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="level">日志等级</param>
        public Warning(string message, string code, LogLevel level) : this(message, code, level, null)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="exception">异常</param>
        public Warning(Exception exception) : this("", "", LogLevel.Warn, exception)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="exception">异常</param>
        public Warning(string message, string code, Exception exception)
            : this(message, code, LogLevel.Warn, exception)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Warning"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="level">日志等级</param>
        /// <param name="exception">异常</param>
        public Warning(string message, string code, LogLevel level, Exception exception)
            : base(message ?? "", exception)
        {
            Code = code;
            Level = level;
            _message = GetMessage();
        }
        #endregion

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志组件</param>
        public void WriteLog(ILog log)
        {
            //switch (Level)
            //{
            //    case LogLevel.Debug:
            //        log.Debug();                    
            //        break;
            //    case LogLevel.Information:
            //        log.Info();
            //        break;
            //    case LogLevel.Warning:
            //        log.Warn();
            //        break;
            //    case LogLevel.Error:
            //        log.Error();
            //        break;
            //    case LogLevel.Fatal:
            //        log.Fatal();
            //        break;
            //}
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志组件</param>
        /// <param name="ex">异常</param>
        public static void WriteLog(ILog log, Exception ex)
        {
            //log.Exception(ex);
            //var exception = ex as Warning;
            //if (exception == null)
            //{
            //    log.Error();
            //    return;
            //}
            //log.ErrorCode(exception.Code);
            //exception.WriteLog(log);
        }

        /// <summary>
        /// 获取友情提示
        /// </summary>
        /// <returns></returns>
        public string GetPrompt()
        {
            switch (Level)
            {
                case LogLevel.Debug:
                    return R.SystemError;
                case LogLevel.Error:
                    return R.SystemError;
            }
            return Message;
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <returns></returns>
        private string GetMessage()
        {
            StringBuilder result=new StringBuilder();
            AppendSelfMessage(result);
            AppendInnerMessage(result,InnerException);
            return result.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        /// <summary>
        /// 添加本身消息
        /// </summary>
        /// <param name="result">拼接器</param>
        private void AppendSelfMessage(StringBuilder result)
        {
            if (string.IsNullOrWhiteSpace(base.Message))
            {
                return;
            }
            result.AppendLine(base.Message);
        }

        /// <summary>
        /// 添加内部异常消息
        /// </summary>
        /// <param name="result">拼接器</param>
        /// <param name="exception">异常</param>
        private void AppendInnerMessage(StringBuilder result, Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            if (exception is Warning)
            {
                result.AppendLine(exception.Message);
                return;
            }
            result.AppendLine(exception.Message);
            result.Append(GetData(exception));
            AppendInnerMessage(result,exception.InnerException);
        }

        /// <summary>
        /// 获取添加的额外数据
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        private string GetData(Exception ex)
        {
            StringBuilder result = new StringBuilder();
            foreach (DictionaryEntry data in ex.Data)
            {
                result.AppendFormat("{0}:{1}{2}", data.Key, data.Value, Environment.NewLine);
            }
            return result.ToString();
        }
    }
}
