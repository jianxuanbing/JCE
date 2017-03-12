/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：StringBuilderExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：33df6379-8817-4db0-a541-edefceb5327a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:54:29
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:54:29
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
    /// StringBuilder扩展
    /// </summary>
    public static class StringBuilderExtensions
    {
        #region TrimStart(去除StringBuilder开头指定值)
        /// <summary>
        /// 去除StringBuilder开头空格
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <returns>返回修改后的StringBuilder，主要用于链式操作</returns>
        public static StringBuilder TrimStart(this StringBuilder sb)
        {
            return sb.TrimStart(' ');
        }
        /// <summary>
        /// 去除StringBuilder开头指定字符
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="c">要去除的字符</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimStart(this StringBuilder sb, char c)
        {
            sb.CheckNotNull("sb");
            if (sb.Length == 0)
            {
                return sb;
            }
            while (c.Equals(sb[0]))
            {
                sb.Remove(0, 1);
            }
            return sb;
        }
        /// <summary>
        /// 去除StringBuilder开头指定字符数组
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="chars">要去除的字符数组</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimStart(this StringBuilder sb, char[] chars)
        {
            chars.CheckNotNull("chars");
            return sb.TrimStart(new string(chars));
        }
        /// <summary>
        /// 去除StringBuilder开头指定字符串
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="str">要去除的字符串</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimStart(this StringBuilder sb, string str)
        {
            sb.CheckNotNull("sb");
            if (string.IsNullOrEmpty(str) || sb.Length == 0 || str.Length > sb.Length)
            {
                return sb;
            }
            while (sb.SubString(0, str.Length).Equals(str))
            {
                sb.Remove(0, str.Length);
                if (str.Length > sb.Length)
                {
                    break;
                }
            }
            return sb;
        }
        #endregion

        #region TrimEnd(去除StringBuilder结尾指定值)
        /// <summary>
        /// 去除StringBuilder结尾的空格
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <returns>返回修改后的StringBuilder，主要用于链式操作</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            return sb.TrimEnd(' ');
        }
        /// <summary>
        /// 去除StringBuilder结尾指定字符
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="c">要去除的字符</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, char c)
        {
            sb.CheckNotNull("sb");
            if (sb.Length == 0)
            {
                return sb;
            }
            while (c.Equals(sb[sb.Length - 1]))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }
        /// <summary>
        /// 去除StringBuilder结尾指定字符数组
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="chars">要去除的字符数组</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, char[] chars)
        {
            chars.CheckNotNull("chars");
            return sb.TrimEnd(new string(chars));
        }
        /// <summary>
        /// 去除StringBuilder结尾指定字符串
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="str">要去除的字符串</param>
        /// <returns>返回修改后的StringBuilder</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, string str)
        {
            sb.CheckNotNull("sb");
            if (string.IsNullOrEmpty(str) || sb.Length == 0 || str.Length > sb.Length)
            {
                return sb;
            }
            while (sb.SubString(sb.Length - str.Length, str.Length).Equals(str))
            {
                sb.Remove(sb.Length - str.Length, str.Length);
                if (sb.Length < str.Length)
                {
                    break;
                }
            }
            return sb;
        }
        #endregion

        #region Trim(去除StringBuilder两端的空格)
        /// <summary>
        /// 去除StringBuilder两端的空格
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <returns>返回修改后的StringBuilder，主要用于链式操作</returns>
        public static StringBuilder Trim(this StringBuilder sb)
        {
            sb.CheckNotNull("sb");
            if (sb.Length == 0)
            {
                return sb;
            }
            return sb.TrimEnd().TrimStart();
        }
        #endregion

        #region SubString(返回StringBuilder从起始位置指定长度的字符串)
        /// <summary>
        /// 返回StringBuilder从起始位置指定长度的字符串
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>字符串</returns>
        /// <exception cref="IndexOutOfRangeException">超出字符串索引长度异常</exception>
        public static string SubString(this StringBuilder sb, int start, int length)
        {
            sb.CheckNotNull("sb");
            if (start + length > sb.Length)
            {
                throw new IndexOutOfRangeException("超出字符串索引长度");
            }
            char[] cs = new char[length];
            for (int i = 0; i < length; i++)
            {
                cs[i] = sb[start + i];
            }
            return new string(cs);
        }
        #endregion

        #region AppendLine(追加格式化字符串参数行)
        /// <summary>
        /// 添加内容并换行
        /// </summary>
        /// <param name="builder">StringBuilder</param>
        /// <param name="value">格式化字符串</param>
        /// <param name="parameters">参数</param>
        public static void AppendLine(this StringBuilder builder, string value, params Object[] parameters)
        {
            builder.AppendLine(string.Format(value, parameters));
        }
        #endregion
    }
}
