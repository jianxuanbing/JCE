/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ValueTypeExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：a7ce0bd1-14e8-43d7-85b5-56ecb978e1b6
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:55:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:55:58
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
    /// 泛型值类型扩展
    /// </summary>
    public static class ValueTypeExtensions
    {
        /// <summary>
        /// 确定指定值是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>bool</returns>
        public static bool IsEmpty<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }
        /// <summary>
        /// 确定指定值是否不为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>bool</returns>
        public static bool IsNotEmpty<T>(this T value) where T : struct
        {
            return (value.IsEmpty() == false);
        }
        /// <summary>
        ///  将指定值转换为相应的可空类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>可空类型</returns>
        public static T? ToNullable<T>(this T value) where T : struct
        {
            return (value.IsEmpty() ? null : (T?)value);
        }
    }
}
