/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Files
 * 文件名：FileUtil
 * 版本号：v1.0.0.0
 * 唯一标识：1cea0aca-22ad-435a-8a9d-92a866fee76e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:12:24
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:12:24
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

namespace JCE.Utils.Files
{
    // 文件工具类 - 流类型转换
    public partial class FileUtil
    {
        #region ToString(转换成字符串)
        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string ToString(Stream data)
        {
            return ToString(data, Const.DefaultEncoding);
        }

        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string ToString(Stream data, Encoding encoding)
        {
            if (data == null)
            {
                return string.Empty;
            }
            string result;
            using (var reader = new StreamReader(data, encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>
        /// <returns></returns>
        public static string ToString(byte[] data)
        {
            return ToString(data, Const.DefaultEncoding);
        }

        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string ToString(byte[] data, Encoding encoding)
        {
            if (data == null || data.Length == 0)
            {
                return string.Empty;
            }
            return encoding.GetString(data);
        }
        #endregion

        #region ToStream(转换成流)
        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static Stream ToStream(string data)
        {
            return ToStream(data, Const.DefaultEncoding);
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static Stream ToStream(string data, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return Stream.Null;
            }
            return new MemoryStream(ToBytes(data, encoding));
        }

        #endregion

        #region ToBytes(转换成字节数组)
        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static byte[] ToBytes(string data)
        {
            return ToBytes(data, Const.DefaultEncoding);
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static byte[] ToBytes(string data, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return new byte[] { };
            }
            return encoding.GetBytes(data);
        }

        /// <summary>
        /// 流转换成字节流
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static byte[] ToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        #endregion

        #region ToInt(转换成整数)
        /// <summary>
        /// 字节数组转换成整数
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static int ToInt(byte[] data)
        {
            if (data == null || data.Length < 4)
            {
                return 0;
            }
            var buffer = new byte[4];
            Buffer.BlockCopy(data, 0, buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }
        #endregion
    }
}
