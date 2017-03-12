/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ThreadExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：885b5326-1a76-4509-96be-de36731b4528
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:55:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:55:07
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 线程（Thread）扩展
    /// </summary>
    public static class ThreadExtensions
    {
        #region CancelSleep(取消线程睡眠状态)
        /// <summary>
        /// 取消线程睡眠状态，继续线程
        /// </summary>
        /// <param name="thread">线程</param>
        public static void CancelSleep(this Thread thread)
        {
            if (thread.ThreadState != ThreadState.WaitSleepJoin)
            {
                return;
            }
            thread.Interrupt();
        }
        #endregion
        #region StartAndIgnoreAbort(启动线程)
        /// <summary>
        /// 启动线程，自动忽略停止线程时触发的ThreadAbortException异常
        /// </summary>
        /// <param name="thread">线程</param>
        /// <param name="failAction">引发非ThreadAbortException异常时执行的逻辑</param>
        public static void StartAndIgnoreAbort(this Thread thread, Action<Exception> failAction = null)
        {
            try
            {
                thread.Start();
            }
            catch (ThreadAbortException)
            { }
            catch (Exception e)
            {
                if (failAction != null)
                {
                    failAction(e);
                }
            }
        }
        #endregion
    }
}
