/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：TaskExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：5c364027-2d92-43d2-ae74-0003c8a687ce
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/24 10:23:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/24 10:23:17
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Task(异步)扩展
    /// </summary>
    public static class TaskExtensions
    {
        #region WaitResult(获取异步操作结果)
        /// <summary>
        /// 获取Task结果，等待完成执行过程时间
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="task">异步操作</param>
        /// <param name="timeoutMillis">超时时间</param>
        /// <returns></returns>
        public static TResult WaitResult<TResult>(this Task<TResult> task, int timeoutMillis)
        {
            if (task.Wait(timeoutMillis))
            {
                return task.Result;
            }
            return default(TResult);
        }
        #endregion

        #region TimeoutAfter(设置异步操作过期时间)
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <param name="task">异步操作</param>
        /// <param name="millisecondsDelay">过期时间</param>
        /// <returns></returns>
        public static async Task TimeoutAfter(this Task task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask =
                await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="task">异步操作</param>
        /// <param name="millisecondsDelay">过期时间</param>
        /// <returns></returns>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask =
                await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return task.Result;
            }
            throw new TimeoutException("The operation has timed out.");
        }
        #endregion

        #region StartDelayedTask(启动异步操作)
        /// <summary>
        /// 启动异步操作
        /// </summary>
        /// <param name="factory">异步操作工厂</param>
        /// <param name="millisecondsDelay">过期时间</param>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public static Task StartDelayedTask(this TaskFactory factory, int millisecondsDelay, Action action)
        {
            //校验参数
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            if (millisecondsDelay < 0)
            {
                throw new ArgumentOutOfRangeException("millisecondsDelay");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            //检查是否已取消标记
            if (factory.CancellationToken.IsCancellationRequested)
            {
                return new Task(() => { }, factory.CancellationToken);
            }

            //创建定时任务
            var tcs = new TaskCompletionSource<object>(factory.CreationOptions);
            var ctr = default(CancellationTokenRegistration);

            //创建计时器但尚未启动它，如果我们现在启动它，它可能会在定时任务前设置为正确的注册
            var timer = new Timer(self =>
            {
                //清除取消标记和计时器，并尝试过渡到完成
                ctr.Dispose();
                ((Timer) self).Dispose();
                tcs.TrySetResult(null);
            });

            //注册取消标记
            if (factory.CancellationToken.CanBeCanceled)
            {
                //取消时，取消计时器并且尝试过渡到取消
                ctr = factory.CancellationToken.Register(() =>
                {
                    timer.Dispose();
                    tcs.TrySetCanceled();
                });
            }

            //启动计时器和返回任务
            try
            {
                timer.Change(millisecondsDelay, Timeout.Infinite);
            }
            catch (ObjectDisposedException)
            {                                
            }
            return tcs.Task.ContinueWith(_ => action(), factory.CancellationToken,
                TaskContinuationOptions.OnlyOnRanToCompletion, factory.Scheduler ?? TaskScheduler.Current);
        }
        #endregion
    }
}
