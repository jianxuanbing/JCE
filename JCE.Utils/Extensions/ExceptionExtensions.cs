/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ExceptionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：8ed27764-b392-46f3-86ec-130af3f82689
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:47:13
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:47:13
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

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 异常（Exception）扩展
    /// </summary>
    public static class ExceptionExtensions
    {
        #region FormatMessage(格式化异常消息)
        /// <summary>
        /// 格式化异常消息
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="isHideStackTrace">是否隐藏异常规模信息</param>
        /// <returns>格式化后的异常信息字符串</returns>
        public static string FormatMessage(this Exception e, bool isHideStackTrace = false)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string appString = string.Empty;
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "  ";
                }
                sb.AppendLine(string.Format("{0}异常消息：{1}", appString, e.Message));
                sb.AppendLine(string.Format("{0}异常类型：{1}", appString, e.GetType().FullName));
                sb.AppendLine(string.Format("{0}异常方法：{1}", appString, (e.TargetSite == null ? null : e.TargetSite.Name)));
                sb.AppendLine(string.Format("{0}异常源：{1}", appString, e.Source));
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(string.Format("{0}异常堆栈：{1}", appString, e.StackTrace));
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(string.Format("{0}内部异常：", appString));
                    count++;
                }
                e = e.InnerException;
            }
            return sb.ToString();
        }
        #endregion
        #region GetOriginalException(获取原始异常)
        /// <summary>
        /// 获取原始异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        [Obsolete("Use GetBaseException instead")]
        public static Exception GetOriginalException(this Exception exception)
        {
            if (exception.InnerException == null)
            {
                return exception;
            }
            return exception.InnerException.GetOriginalException();
        }
        #endregion
        #region Messages(获取所有错误消息列表)
        /// <summary>
        /// 获取所有错误消息列表
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        /// <note>
        /// 最内部的异常消息在列表中第一项，最外层的异常消息在列表中最后一项
        /// </note>
        public static IEnumerable<string> Messages(this Exception exception)
        {
            return exception != null
                ? new List<string>(exception.InnerException.Messages()) { exception.Message }
                : Enumerable.Empty<string>();
        }
        #endregion
        #region Exceptions(获取所有异常列表)
        /// <summary>
        /// 获取所有异常列表
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public static IEnumerable<Exception> Exceptions(this Exception exception)
        {
            return exception != null
                ? new List<Exception>(exception.InnerException.Exceptions
                    ()) { exception }
                : Enumerable.Empty<Exception>();
        }
        #endregion
    }
}
