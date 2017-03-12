/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Exceptions
 * 文件名：ExceptionUtil
 * 版本号：v1.0.0.0
 * 唯一标识：8dd9200f-2d96-4ebf-af1b-03665b4ddff5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:20:52
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:20:52
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

namespace JCE.Utils.Exceptions
{
    /// <summary>
    /// 异常工具类
    /// </summary>
    public class ExceptionUtil
    {
        #region IgnoreException(忽略异常)
        /// <summary>
        /// 忽略异常
        /// </summary>
        /// <param name="action">操作</param>
        public static void IgnoreException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 忽略异常
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">操作</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T IgnoreException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion
    }
}
