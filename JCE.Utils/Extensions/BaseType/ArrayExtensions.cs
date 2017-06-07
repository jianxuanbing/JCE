/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ArrayExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b868ea25-d6c0-4582-951a-8d0fa5cdbf6d
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 21:53:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 21:53:50
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 数组（Array）扩展
    /// </summary>
    public static class ArrayExtensions
    {
        #region IsNullOrEmpty(判断数组是否为空)
        /// <summary>
        /// 判断数组是否为空
        /// </summary>
        /// <param name="source">Array</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this Array source)
        {
            return source == null || source.Length == 0;
        }
        #endregion

        #region WithinIndex(判断索引是否在数组中)
        /// <summary>
        /// 判断索引是否在数组中
        /// </summary>
        /// <param name="source">Array</param>
        /// <param name="index">索引</param>
        /// <returns>bool</returns>
        public static bool WithinIndex(this Array source, int index)
        {
            return source != null && index >= 0 && index < source.Length;
        }
        /// <summary>
        /// 判断索引是否在数组中
        /// </summary>
        /// <param name="source">Array</param>
        /// <param name="index">索引</param>
        /// <param name="dimension">范围</param>
        /// <returns>bool</returns>
        public static bool WithinIndex(this Array source, int index, int dimension)
        {
            return source != null && index >= source.GetLowerBound(dimension) &&
                   index <= source.GetUpperBound(dimension);
        }
        #endregion

        #region CombineArray(合并两个数组)
        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="combineWith">源数组</param>
        /// <param name="arrayToCombine">目标数组</param>
        /// <returns>合并后的数组</returns>
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                int initialSize = combineWith.Length;
                Array.Resize(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize,
                    arrayToCombine.Length);
            }
            return combineWith;
        }
        #endregion

        #region ClearAll(清空数组)
        /// <summary>
        /// 清空数组
        /// </summary>
        /// <param name="clear">需要清空内容的数组</param>
        /// <returns>清空内容后的数组</returns>
        public static Array ClearAll(this Array clear)
        {
            if (clear != null)
            {
                Array.Clear(clear, 0, clear.Length);
            }
            return clear;
        }
        /// <summary>
        /// 清空数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="arrayToClear">需要清空内容的数组</param>
        /// <returns>清空内容后的数组</returns>
        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
            {
                for (int i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                {
                    arrayToClear[i] = default(T);
                }
            }
            return arrayToClear;
        }
        #endregion

        #region ClearAt(清空数组中指定项内容)
        /// <summary>
        /// 清除数组中指定项内容
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="at">指定项索引</param>
        /// <returns>清除指定项后的数组</returns>
        public static Array ClearAt(this Array array, int at)
        {
            if (array != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(array))
                {
                    Array.Clear(array, arrayIndex, 1);
                }
            }
            return array;
        }
        /// <summary>
        /// 清除数组中指定项内容
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">T[]</param>
        /// <param name="at">指定项索引</param>
        /// <returns>清除指定项后的数组</returns>
        public static T[] ClearAt<T>(this T[] array, int at)
        {
            if (array != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(array))
                {
                    array[arrayIndex] = default(T);
                }
            }
            return array;
        }
        #endregion

        #region BlockCopy(数据块复制)
        /// <summary>
        /// 数据块复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <returns>复制后的数组</returns>
        public static T[] BlockCopy<T>(this T[] array, int index, int length)
        {
            return BlockCopy(array, index, length, false);
        }
        /// <summary>
        /// 数据块复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <param name="padToLength">是否指定长度</param>
        /// <returns>复制后的数组</returns>
        public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }
            int n = length;
            T[] b = null;
            if (array.Length < index + length)//5,3,10
            {
                n = array.Length - index;//n=array数组剩余长度
                if (padToLength)
                {
                    b = new T[length];
                }
            }
            if (b == null)
            {
                b = new T[n];
            }
            Array.Copy(array, index, b, 0, n);//从array数组指定索引开始复制数据到b数组当中，直至到达指定长度结束复制
            return b;
        }
        /// <summary>
        /// 数据块复制，允许枚举在数组中复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="count">总数</param>
        /// <param name="padToLength">是否指定长度</param>
        /// <returns>复制后的数组</returns>
        public static IEnumerable<T[]> BlockCopy<T>(this T[] array, int count, bool padToLength = false)
        {
            for (int i = 0; i < array.Length; i += count)
            {
                yield return array.BlockCopy(i, count, padToLength);
            }
        }

        #endregion

        #region FindArrayInArray(在byte[]数组中查找指定byte[]数组)
        /// <summary>
        /// 在byte[]数组中查找指定byte[]数组，返回第一次匹配索引
        /// </summary>
        /// <param name="buf1">byte[]数组源</param>
        /// <param name="buf2">指定查询byte[]数组</param>
        /// <returns>返回第一次匹配byte[]数组的位置，如果没有找到则返回-1</returns>
        public static int FindArrayInArray(this byte[] buf1, byte[] buf2)
        {
            if (buf2 == null)
                throw new ArgumentNullException("buf2");

            if (buf1 == null)
                throw new ArgumentNullException("buf1");

            if (buf2.Length == 0)
                return 0;		// by definition empty sets match immediately

            int j = -1;
            int end = buf1.Length - buf2.Length;
            while ((j = Array.IndexOf(buf1, buf2[0], j + 1)) <= end && j != -1)
            {
                int i = 1;
                while (buf1[j + i] == buf2[i])
                {
                    if (++i == buf2.Length)
                        return j;
                }
            }
            return -1;
        }
        #endregion

        #region ToString(字符串数组转换为字符串)
        /// <summary>
        /// 字符串数组转换为字符串，组合字符串数组
        /// </summary>
        /// <param name="values">字符串数组</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <param name="quotation">引号</param>
        /// <param name="separator">逗号</param>
        /// <returns>字符串</returns>
        public static string ToString(this string[] values, string prefix = "(", string suffix = ")", string quotation = "\"", string separator = ",")
        {
            var sb = new StringBuilder();
            sb.Append(prefix);

            for (var i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    sb.Append(separator);
                if (quotation != null)
                    sb.Append(quotation);
                sb.Append(values[i]);
                if (quotation != null)
                    sb.Append(quotation);
            }

            sb.Append(suffix);
            return sb.ToString();
        }
        /// <summary>
        /// 将任何数组转换成符号连接的字符串
        /// </summary>
        /// <typeparam name="T">基本对象</typeparam>
        /// <param name="obj">任何对象</param>
        /// <param name="sign">分隔符</param>
        /// <param name="func">传入要在转换过程中执行的方法</param>
        /// <returns></returns>
        public static string ToString<T>(this T[] obj, string sign = ",", Func<T, string> func = null)
        {
            if (obj.IsNullOrEmpty())
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            foreach (var t in obj)
            {
                if (func == null)
                {
                    str.AppendFormat(sign + t);
                }
                else
                {
                    str.Append(sign + func(t));
                }
            }
            return str.ToString().Substring(sign.Length);
        }
        #endregion

        #region ToArray(List集合转换为数组)
        /// <summary>
        /// List集合转换成数组
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="list">List集合</param>
        /// <returns></returns>
        public static T[] ToArray<T>(this List<T> list)
        {
            T[] array = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }
        #endregion

        #region Remove(指定清除标签的内容)
        /// <summary>
        /// 指定清除标签的内容
        /// </summary>
        /// <param name="strs">内容</param>
        /// <param name="tag">标签</param>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public static string[] Remove(this string[] strs, string tag, RegexOptions options = RegexOptions.None)
        {
            for (var i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i].Remove(tag, options);
            }
            return strs;
        }
        #endregion

        #region Multiply(数组相乘)

        /// <summary>
        /// 两个数组相乘，返回两个的连接串，例如：["a","b","c"] * ["A","B"] = ["aA","aB","bA","bB","cA","cB"]
        /// </summary>
        /// <param name="array">当前字符串数组</param>
        /// <param name="other">其他字符串数组</param>
        /// <returns></returns>
        public static string[] Multiply(this string[] array, string[] other)
        {
            if (array == null || other == null)
            {
                return null;
            }
            var temp = new string[array.Length*other.Length];
            var i = 0;
            foreach (string item in array)
            {
                foreach (string s in other)
                {
                    temp[i++] = item + s;
                }
            }
            return temp;
        }
        #endregion

        #region ToConvertAll(转换数组类型)

        /// <summary>
        /// 转换数组类型
        /// </summary>
        /// <typeparam name="TInput">输入类型</typeparam>
        /// <typeparam name="TResult">输出类型</typeparam>
        /// <param name="source">源数组</param>
        /// <param name="convert">转换器</param>
        /// <returns></returns>
        public static TResult[] ToConvertAll<TInput, TResult>(this TInput[] source, Converter<TInput, TResult> convert)
        {
            return Array.ConvertAll(source, convert);
        }
        #endregion
    }
}
