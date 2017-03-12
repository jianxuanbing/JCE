/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ReaderWriterLockSlimExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：acf53bba-f014-49cf-b62c-91ae5b4bbf13
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/24 10:06:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/24 10:06:00
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
    /// ReaderWriterLockSlim(读写锁)扩展
    /// </summary>
    public static class ReaderWriterLockSlimExtensions
    {
        #region ReadOnly(只读)
        /// <summary>
        /// 只读操作
        /// </summary>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="action">操作</param>
        public static void ReadOnly(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            readerWriterLockSlim.EnterReadLock();
            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 只读操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="function">Lambda表达式</param>
        /// <returns></returns>
        public static T ReadOnly<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }
            readerWriterLockSlim.EnterReadLock();
            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        #endregion
        #region WriteOnly(只写)
        /// <summary>
        /// 只写操作
        /// </summary>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="action">操作</param>
        public static void WriteOnly(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
        /// <summary>
        /// 只写操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="readerWriterLockSlim">读写锁</param>
        /// <param name="function">Lambda表达式</param>
        /// <returns></returns>
        public static T WriteOnly<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        #endregion
    }
}
