/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：Extensions.Convert
 * 版本号：v1.0.0.0
 * 唯一标识：66800eff-b33f-432a-9fa0-21d3e4925cdd
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/19 21:37:01
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/19 21:37:01
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
using System.Web.UI.WebControls;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static partial class Extensions
    {
        #region ToBool(转换为bool)
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static bool ToBool(this string obj)
        {
            return Conv.ToBool(obj);
        }

        /// <summary>
        /// 转换为可空bool类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static bool? ToBoolOrNull(this string obj)
        {
            return Conv.ToBoolOrNull(obj);
        }
        #endregion

        #region ToInt(转换为int)
        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static int ToInt(this string obj)
        {
            return Conv.ToInt(obj);
        }

        /// <summary>
        /// 转换为可空int类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string obj)
        {
            return Conv.ToIntOrNull(obj);
        }
        #endregion

        #region ToLong(转换为long)
        /// <summary>
        /// 转换为long类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static long ToLong(this string obj)
        {
            return Conv.ToLong(obj);
        }

        /// <summary>
        /// 转换为可空long类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static long? ToLongOrNull(this string obj)
        {
            return Conv.ToLongOrNull(obj);
        }
        #endregion

        #region ToDouble(转换为double)
        /// <summary>
        /// 转换为double类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static double ToDouble(this string obj)
        {
            return Conv.ToDouble(obj);
        }

        /// <summary>
        /// 转换为可空double类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(this string obj)
        {
            return Conv.ToDoubleOrNull(obj);
        }
        #endregion

        #region ToDecimal(转换为decimal)
        /// <summary>
        /// 转换为decimal类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string obj)
        {
            return Conv.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为可空decimal类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(this string obj)
        {
            return Conv.ToDecimalOrNull(obj);
        }
        #endregion

        #region ToDate(转换为DateTime)
        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static DateTime ToDate(this string obj)
        {
            return Conv.ToDate(obj);
        }

        /// <summary>
        /// 转换为可空DateTime类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static DateTime? ToDateOrNull(this string obj)
        {
            return Conv.ToDateOrNull(obj);
        }
        #endregion

        #region ToGuid(转换为Guid)
        /// <summary>
        /// 转换为Guid类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static Guid ToGuid(this string obj)
        {
            return Conv.ToGuid(obj);
        }

        /// <summary>
        /// 转换为可空Guid类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public static Guid? ToGuidOrNull(this string obj)
        {
            return Conv.ToGuidOrNull(obj);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="obj">数据,范例: "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A"</param>
        /// <returns></returns>
        public static List<Guid> ToGuidList(this string obj)
        {
            return Conv.ToGuidList(obj);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="obj">字符串集合</param>
        /// <returns></returns>
        public static List<Guid> ToGuidList(this IList<string> obj)
        {
            if (obj == null)
            {
                return new List<Guid>();
            }
            return obj.Select(t => t.ToGuid()).ToList();
        }
        #endregion

        #region ToStr(转换为字符串)
        /// <summary>
        /// 获取字符串，当值为null时返回""
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToStr(this object obj)
        {
            return Conv.ToString(obj);
        }
        #endregion
        
    }
}
