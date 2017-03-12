/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：ThreadUtil
 * 版本号：v1.0.0.0
 * 唯一标识：d75145fe-a7a3-48d8-84dc-cd0d89e3a56c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:24:23
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:24:23
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 线程操作工具类
    /// </summary>
    public class ThreadUtil
    {
        #region ThreadId(获取线程编号)
        /// <summary>
        /// 获取线程编号
        /// </summary>
        public static string ThreadId
        {
            get { return Thread.CurrentThread.ManagedThreadId.ToString(); }
        }
        #endregion

        #region CurrentPrincipal(获取当前安全主体)
        /// <summary>
        /// 获取当前安全主体
        /// </summary>
        public static IPrincipal CurrentPrincipal
        {
            get { return Thread.CurrentPrincipal; }
            set { Thread.CurrentPrincipal = value; }
        }
        #endregion

        #region Sleep(线程等待)
        /// <summary>
        /// 线程等待
        /// </summary>
        /// <param name="time">等待时间，单位:毫秒</param>
        public static void Sleep(int time)
        {
            Thread.Sleep(time);
        }
        #endregion

        #region MaxThreadNumberInThreadPool(获取线程池中最大线程数)
        /// <summary>
        /// 获取线程池中最大线程数
        /// </summary>
        public static int MaxThreadNumberInThreadPool
        {
            get
            {
                int maxNumber;
                int ioNumber;
                ThreadPool.GetMaxThreads(out maxNumber, out ioNumber);
                return maxNumber;
            }
        }
        #endregion

        #region StartTask(启动异步任务)
        /// <summary>
        /// 启动异步任务
        /// </summary>
        /// <param name="handler">任务，范例：() => { 代码 }</param>
        public static void StartTask(Action handler)
        {
            Task.Factory.StartNew(handler);
        }
        /// <summary>
        /// 启动异步任务
        /// </summary>
        /// <param name="handler">任务，范例：t => { 代码 }</param>
        /// <param name="state">传递的参数</param>
        public static void StartTask(Action<object> handler, object state)
        {
            Task.Factory.StartNew(handler, state);
        }
        #endregion
    }
}
