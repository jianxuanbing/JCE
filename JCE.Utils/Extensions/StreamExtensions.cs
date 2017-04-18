/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：StreamExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：8b0080a3-d1da-44df-8a27-f6dd6e5d3a23
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:54:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:54:12
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 字节流（Stream）扩展
    /// </summary>
    public static class StreamExtensions
    {
        #region GetReader(获取流读取器)
        /// <summary>
        /// 获取流读取器，使用默认编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>流读取器</returns>
        public static StreamReader GetReader(this Stream stream)
        {
            return stream.GetReader(null);
        }
        /// <summary>
        /// 获取流读取器，使用指定编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">编码</param>
        /// <returns>流读取器</returns>
        public static StreamReader GetReader(this Stream stream, Encoding encoding)
        {
            if (stream.CanRead == false)
            {
                throw new InvalidOperationException("Stream does not support reading.");//流不支持读取
            }
            encoding = (encoding ?? Encoding.Default);
            return new StreamReader(stream, encoding);
        }
        #endregion
        #region GetWriter(获取流写入器)
        /// <summary>
        /// 获取流写入器，使用默认编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>流写入器</returns>
        public static StreamWriter GetWriter(this Stream stream)
        {
            return stream.GetWriter(null);
        }
        /// <summary>
        /// 获取流写入器，使用指定编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">编码</param>
        /// <returns>流写入器</returns>
        public static StreamWriter GetWriter(this Stream stream, Encoding encoding)
        {
            if (stream.CanWrite == false)
                throw new InvalidOperationException("Stream does not support writing.");//流不支持写入

            encoding = (encoding ?? Encoding.Default);
            return new StreamWriter(stream, encoding);
        }
        #endregion
        #region ReadToEnd(读取所有文本)
        /// <summary>
        /// 从流中读取所有文本，使用默认编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>文本</returns>
        public static string ReadToEnd(this Stream stream)
        {
            return stream.ReadToEnd(null);
        }
        /// <summary>
        /// 从流中读取所有文本，使用指定编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">编码</param>
        /// <returns>文本</returns>
        public static string ReadToEnd(this Stream stream, Encoding encoding)
        {
            using (var reader = stream.GetReader(encoding))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion
        #region SeekToBegin(设置流指针指向流的开始位置)
        /// <summary>
        /// 设置流指针指向流的开始位置
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>流</returns>
        public static Stream SeekToBegin(this Stream stream)
        {
            if (stream.CanSeek == false)
                throw new InvalidOperationException("Stream does not support seeking.");//流不支持查找
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        #endregion
        #region SeekToEnd(设置流指针指向流的结束位置)
        /// <summary>
        /// 设置流指针指向流的结束位置
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>流</returns>
        public static Stream SeekToEnd(this Stream stream)
        {
            if (stream.CanSeek == false)
                throw new InvalidOperationException("Stream does not support seeking.");//流不支持查找
            stream.Seek(0, SeekOrigin.End);
            return stream;
        }
        #endregion
        #region CopyToMemory(复制流到内存流)
        /// <summary>
        /// 将流复制到内存流中
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>内存流</returns>
        public static MemoryStream CopyToMemory(this Stream stream)
        {
            var memoryStream = new MemoryStream((int)stream.Length);
            stream.CopyTo(memoryStream);
            return memoryStream;
        }
        #endregion
        #region ReadAllBytes(将流写入字节数组)
        /// <summary>
        /// 将流写入字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using (var memoryStream = stream.CopyToMemory())
                return memoryStream.ToArray();
        }
        #endregion
        #region ReadFixedBuffersize(将指定缓冲大小的流写入字节数组)
        /// <summary>
        /// 将指定缓冲大小的流写入字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="bufsize">指定缓存大小</param>
        /// <returns>字节数组</returns>
        public static byte[] ReadFixedBuffersize(this Stream stream, int bufsize)
        {
            var buf = new byte[bufsize];
            int offset = 0, cnt;
            do
            {
                cnt = stream.Read(buf, offset, bufsize - offset);
                if (cnt == 0)
                    return null;
                offset += cnt;
            } while (offset < bufsize);

            return buf;
        }
        #endregion
        #region Write(将字节数组写入流)
        /// <summary>
        /// 将字节数组写入流
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="bytes">字节数组</param>
        public static void Write(this Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
        #endregion
        #region ToDeserializeBinary(将二进制流反序列化成对象)
        /// <summary>
        /// 将二进制流反序列化成对象
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static object ToDeserializeBinary(this Stream stream)
        {
            IFormatter formatter=new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        /// <summary>
        /// 将二进制流反序列化成泛型对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static T ToDeserializeBinary<T>(this Stream stream) where T : class
        {
            return stream.ToDeserializeBinary() as T;
        }
        #endregion
    }
}
