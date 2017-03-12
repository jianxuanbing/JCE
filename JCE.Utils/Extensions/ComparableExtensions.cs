/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ComparableExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：0b24d48c-b062-4837-b349-1f9a8fc7b0d2
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:39:56
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:39:56
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
    /// 比较对象（string,Datetime,numeric）扩展
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// 判断指定值是否在定义的最小值和最大值（包括边界值）之间
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>如果指定值介于最小值和最大值之间返回true</returns>
        public static bool IsBetWeen<T>(this T value, T minValue, T maxValue) where T : IComparer<T>
        {
            return value.IsBetWeen(minValue, maxValue, Comparer<T>.Default);
        }
        /// <summary>
        /// 判断指定值是否在定义的最小值和最大值（包括边界值）之间
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="comparer">比较器</param>
        /// <returns>如果指定值介于最小值和最大值之间返回true</returns>
        public static bool IsBetWeen<T>(this T value, T minValue, T maxValue, IComparer<T> comparer) where T : IComparer<T>
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            var minMaxCompare = comparer.Compare(minValue, maxValue);
            if (minMaxCompare < 0)
            {
                return ((comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0));
            }
            else
            {
                return ((comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0));
            }
        }
        /// <summary>
        /// 降序比较器
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        public class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
        {
            /// <summary>
            /// 对象比较
            /// </summary>
            /// <param name="x">对象</param>
            /// <param name="y">对象</param>
            /// <returns></returns>
            public int Compare(T x, T y)
            {
                return y.CompareTo(x);
            }
        }
        /// <summary>
        /// 升序比较器
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        public class AscendingComparer<T> : IComparer<T> where T : IComparable<T>
        {
            /// <summary>
            /// 对象比较
            /// </summary>
            /// <param name="x">对象</param>
            /// <param name="y">对象</param>
            /// <returns></returns>
            public int Compare(T x, T y)
            {
                return x.CompareTo(y);
            }
        }
    }
}
