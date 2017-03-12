/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：TryCatchExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：88f31ad6-ed6b-4daa-a152-4781b68f254f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:55:26
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:55:26
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
    /// Try-Catch扩展
    /// </summary>
    public static class TryCatchExtensions
    {
        #region TryCatch(对某对象执行指定功能与后续功能，并处理异常情况)
        /// <summary>
        /// 对某对象执行指定功能与后续功能，并处理异常情况
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="action">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <param name="successAction">主功能代码成功后执行的功能代码</param>
        /// <returns>主功能代码是否顺利执行</returns>
        public static bool TryCatch<T>(this T source, Action<T> action, Action<Exception> failureAction, Action<T> successAction) where T : class
        {
            bool result;
            try
            {
                action(source);
                successAction(source);
                result = true;
            }
            catch (Exception obj)
            {
                failureAction(obj);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 对某对象执行指定功能，并处理异常情况
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="action">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <returns>主功能代码是否顺利执行</returns>
        public static bool TryCatch<T>(this T source, Action<T> action, Action<Exception> failureAction) where T : class
        {
            return source.TryCatch(action,
                failureAction,
                obj =>
                { });
        }

        /// <summary>
        /// 对某对象执行指定功能，并处理异常情况与返回值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="func">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <param name="successAction">主功能代码成功后执行的功能代码</param>
        /// <returns>功能代码的返回值，如果出现异常，则返回对象类型的默认值</returns>
        public static TResult TryCatch<T, TResult>(this T source, Func<T, TResult> func, Action<Exception> failureAction, Action<T> successAction)
            where T : class
        {
            TResult result;
            try
            {
                TResult u = func(source);
                successAction(source);
                result = u;
            }
            catch (Exception obj)
            {
                failureAction(obj);
                result = default(TResult);
            }
            return result;
        }

        /// <summary>
        /// 对某对象执行指定功能，并处理异常情况与返回值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">值</param>
        /// <param name="func">要对值执行的主功能代码</param>
        /// <param name="failureAction">catch中的功能代码</param>
        /// <returns>功能代码的返回值，如果出现异常，则返回对象类型的默认值</returns>
        public static TResult TryCatch<T, TResult>(this T source, Func<T, TResult> func, Action<Exception> failureAction) where T : class
        {
            return source.TryCatch(func,
                failureAction,
                obj =>
                { });
        }
        #endregion
    }
}
