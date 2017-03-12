/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：BooleanExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：4a3dc90d-183c-49c4-afa2-990ca38767f7
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:03:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:03:38
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
    /// bool类型的扩展辅助操作类
    /// </summary>
    public static class BooleanExtensions
    {
        #region ToLower(将布尔值转换为小写字符串)
        /// <summary>
        /// 将布尔值转换为小写字符串
        /// </summary>
        /// <param name="value">bool</param>
        /// <returns>小写字符串</returns>
        public static string ToLower(this bool value)
        {
            return value.ToString().ToLower();
        }
        #endregion

        #region ToYesNoString(将布尔值转换为等效的字符串表示形式)
        /// <summary>
        /// 将布尔值转换为等效的字符串表示形式（Yes、No）
        /// </summary>
        /// <param name="value">bool</param>
        /// <returns>等效的字符串表示形式</returns>
        public static string ToYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }
        #endregion

        #region ToBinaryTypeNumber(将布尔值转换为二进制数字类型)
        /// <summary>
        /// 将布尔值转换为二进制数字类型，1:0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToBinaryTypeNumber(this bool value)
        {
            return value ? 1 : 0;
        }
        #endregion

        #region IsTrue(结果为true时，输出参数)
        /// <summary>
        /// 结果为True时，输出参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b">判断源结果</param>
        /// <param name="t">输出值</param>
        /// <returns></returns>
        public static T IsTrue<T>(this bool b, T t)
        {
            return b ? t : default(T);
        }

        /// <summary>
        /// 结果为True时，输出参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b">判断源结果</param>
        /// <param name="t">输出值</param>
        /// <returns></returns>
        public static T IsTrue<T>(this bool? b, T t)
        {
            return b.GetValueOrDefault() ? t : default(T);
        }
        #endregion

        #region IsFalse(结果为False时，输出参数)
        /// <summary>
        /// 结果为False时，输出参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b">判断源结果</param>
        /// <param name="t">输出值</param>
        /// <returns></returns>
        public static T IsFalse<T>(this bool b, T t)
        {
            return !b ? t : default(T);
        }

        /// <summary>
        /// 结果为False时，输出参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b">判断源结果</param>
        /// <param name="t">输出值</param>
        /// <returns></returns>
        public static T IsFalse<T>(this bool? b, T t)
        {
            return !b.GetValueOrDefault() ? t : default(T);
        }
        #endregion

        #region GetName(获取中文)

        /// <summary>
        /// 获取中文
        /// </summary>
        /// <param name="b">判断源</param>
        /// <param name="strTrue">为True时的中文:是</param>
        /// <param name="strFalse">为Flase时的中文:否</param>
        /// <returns></returns>
        public static string GetName(this bool b, string strTrue = "是", string strFalse = "否")
        {
            return b ? strTrue : strFalse;
        }

        /// <summary>
        /// 获取中文
        /// </summary>
        /// <param name="b">判断源</param>
        /// <param name="strTrue">为True时的中文:是</param>
        /// <param name="strFalse">为Flase时的中文:否</param>
        /// <returns></returns>
        public static string GetName(this bool? b, string strTrue = "是", string strFlase = "否")
        {
            return b.GetValueOrDefault() ? strTrue : strFlase;
        }
        #endregion
    }
}
