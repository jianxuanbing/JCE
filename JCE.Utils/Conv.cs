/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Conv
 * 版本号：v1.0.0.0
 * 唯一标识：e8e25c81-22e8-406e-b97e-e20ef2f6c740
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:09:51
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:09:51
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class Conv
    {
        #region ToNum(数值转换)
        #region ToInt(转换为int)
        /// <summary>
        /// 转换成int类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static int ToInt(object data)
        {
            int result;
            var success = int.TryParse(data.ToStr(), out result);
            if (success)
            {
                return result;
            }
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换为可空int类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static int? ToIntOrNull(object data)
        {
            int result;
            return int.TryParse(data.ToStr(), out result) ? (int?)result : null;
        }
        #endregion

        #region ToLong(转换为long)
        /// <summary>
        /// 转换为long类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static long ToLong(object data)
        {
            long result;
            var success = long.TryParse(data.ToStr(), out result);
            if (success)
            {
                return result;
            }
            try
            {
                return Convert.ToInt64(ToDecimal(data, 0));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空long类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static long? ToLongOrNull(object data)
        {
            long result;
            return long.TryParse(data.ToStr(), out result) ? (long?)result : null;
        }
        #endregion

        #region ToDouble(转换为double)
        /// <summary>
        /// 转换为double类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static double ToDouble(object data)
        {
            double result;
            return double.TryParse(data.ToStr(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为double类型,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static double ToDouble(object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为可空double类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(object data)
        {
            double result;
            return double.TryParse(data.ToStr(), out result) ? (double?)result : null;
        }
        #endregion

        #region ToDecimal(转换为decimal)
        /// <summary>
        /// 转换为decimal类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static decimal ToDecimal(object data)
        {
            decimal result;
            return decimal.TryParse(data.ToStr(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为decimal类型,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static decimal ToDecimal(object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits);
        }

        /// <summary>
        /// 转换为可空decimal类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(object data)
        {
            decimal result;
            return decimal.TryParse(data.ToStr(), out result) ? (decimal?)result : null;
        }

        /// <summary>
        /// 转换为可空decimal类型,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
            {
                return null;
            }
            return Math.Round(result.Value, digits);
        }
        #endregion        
        #endregion

        #region ToGuid(Guid转换)
        /// <summary>
        /// 转换为Guid类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static Guid ToGuid(object data)
        {
            Guid result;
            return Guid.TryParse(data.ToStr(), out result) ? result : Guid.Empty;
        }

        /// <summary>
        /// 转换为可空Guid类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static Guid? ToGuidOrNull(object data)
        {
            Guid result;
            return Guid.TryParse(data.ToStr(), out result) ? (Guid?)result : null;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="list">guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        /// <returns></returns>
        public static List<Guid> ToGuidList(string list)
        {
            return ToList<Guid>(list);
        }
        #endregion

        #region ToDate(日期转换)
        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static DateTime ToDate(object data)
        {
            DateTime result;
            return DateTime.TryParse(data.ToStr(), out result) ? result : DateTime.MinValue;
        }
        /// <summary>
        /// 转换为可空DateTime类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static DateTime? ToDateOrNull(object data)
        {
            DateTime result;
            return DateTime.TryParse(data.ToStr(), out result) ? (DateTime?)result : null;
        }
        #endregion

        #region ToBool(转换为bool)
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static bool ToBool(object data)
        {
            bool? value = GetBool(data);
            if (value != null)
            {
                return value.Value;
            }
            bool result;
            return bool.TryParse(data.ToStr(), out result) && result;
        }

        /// <summary>
        /// 转换为可空bool类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static bool? ToBoolOrNull(object data)
        {
            bool? value = GetBool(data);
            if (value != null)
            {
                return value.Value;
            }
            bool result;
            return bool.TryParse(data.ToStr(), out result) ? (bool?)result : null;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private static bool? GetBool(object data)
        {
            switch (data.ToStr().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }
        #endregion

        #region ToString(字符串转换)
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string ToString(object data)
        {
            return data == null ? string.Empty : data.ToString().Trim();
        }
        #endregion

        #region To(通用转换)
        /// <summary>
        /// 转换为目标元素
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static T To<T>(object data)
        {
            if (data == null)
            {
                return default(T);
            }
            if (data is string && string.IsNullOrWhiteSpace(data.ToString()))
            {
                return default(T);
            }
            Type type = Sys.GetType<T>();
            try
            {
                if (type.Name.ToLower() == "guid")
                {
                    return (T)(object)new Guid(data.ToString());
                }
                if (data is IConvertible)
                {
                    return (T)Convert.ChangeType(data, type);
                }
                return (T)data;
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 转换为目标元素集合
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="list">元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        /// <returns></returns>
        public static List<T> ToList<T>(string list)
        {
            var result = new List<T>();
            if (string.IsNullOrWhiteSpace(list))
            {
                return result;
            }
            var array = list.Split(',');
            result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
            return result;
        }
        #endregion

        #region ToBytes(转换为byte[])
        /// <summary>
        /// 将Stream转换成byte[]
        /// </summary>
        /// <param name="stream">内存流</param>
        /// <param name="bufferLen">缓存长度</param>
        /// <returns></returns>
        public static byte[] ToBytes(Stream stream, int bufferLen = 0)
        {
            if (bufferLen < 1)
            {
                bufferLen = 0x8000;
            }
            byte[] buffer = new byte[bufferLen];
            int read = 0;
            int block;
            //每次从流中读取缓存大小的数据 直到读取完所有的流为止
            while ((block = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                //重新设置读取位置
                read += block;
                //检查是否到达了缓存的边界，检查是否还有可以读取的信息
                if (read == buffer.Length)
                {
                    //尝试读取一个字节
                    int nextByte = stream.ReadByte();
                    // 读取失败则说明读取完成可以返回结果
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    // 调整数组大小准备继续读取
                    byte[] newBuf = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuf, buffer.Length);
                    newBuf[read] = (byte)nextByte;
                    // buffer是一个引用（指针），这里意在重新设定buffer指针指向一个更大的内存
                    buffer = newBuf;
                    read++;
                }
            }
            return buffer;
        }
        #endregion

        #region ToStream(转换成流)
        /// <summary>
        /// 转换成流
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <returns></returns>
        public static Stream ToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion
    }
}
