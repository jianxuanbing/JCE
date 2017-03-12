/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：ByteUtil
 * 版本号：v1.0.0.0
 * 唯一标识：bf67dd1a-4a51-44dd-814b-8f56dd01c73f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:25:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:25:50
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

namespace JCE.Utils.Common
{
    /// <summary>
    /// 字节操作工具类
    /// </summary>
    public class ByteUtil
    {
        #region DecodeString(解码为string)
        /// <summary>
        /// 从byte数组中取出string字符串，消息的格式为[消息长度,int 4字节][消息内容]
        /// </summary>
        /// <param name="sourceBuffer">源字节</param>
        /// <param name="startOffset">开始位置</param>
        /// <param name="nextStartOffset">下一步开始位置</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string DecodeString(byte[] sourceBuffer, int startOffset, out int nextStartOffset,
            Encoding encoding = default(Encoding))
        {
            return Encoding.UTF8.GetString(DecodeBytes(sourceBuffer, startOffset, out nextStartOffset));
        }
        #endregion

        #region DecodeInt(解码为int)
        /// <summary>
        /// 从byte数组中取出int数字
        /// </summary>
        /// <param name="sourceBuffer">源字节</param>
        /// <param name="startOffset">开始位置</param>
        /// <param name="nextStartOffset">下一步开始位置</param>
        /// <returns></returns>
        public static int DecodeInt(byte[] sourceBuffer, int startOffset, out int nextStartOffset)
        {
            var intBytes = new byte[4];
            Buffer.BlockCopy(sourceBuffer, startOffset, intBytes, 0, 4);
            nextStartOffset = startOffset + 4;
            return BitConverter.ToInt32(intBytes, 0);
        }
        #endregion

        #region DecodeLong(解码为long)
        /// <summary>
        /// 从byte数组中取出long数字
        /// </summary>
        /// <param name="sourceBuffer">源字节</param>
        /// <param name="startOffset">开始位置</param>
        /// <param name="nextStartOffset">下一步开始位置</param>
        /// <returns></returns>
        public static long DecodeLong(byte[] sourceBuffer, int startOffset, out int nextStartOffset)
        {
            var longBytes = new byte[8];
            Buffer.BlockCopy(sourceBuffer, startOffset, longBytes, 0, 8);
            nextStartOffset = startOffset + 8;
            return BitConverter.ToInt64(longBytes, 0);
        }
        #endregion

        #region DecodeDateTime(解码为DateTime)
        /// <summary>
        /// 从byte数组中取出DateTime时间
        /// </summary>
        /// <param name="sourceBuffer">源字节</param>
        /// <param name="startOffset">开始位置</param>
        /// <param name="nextStartOffset">下一步开始位置</param>
        /// <returns></returns>
        public static DateTime DecodeDateTime(byte[] sourceBuffer, int startOffset, out int nextStartOffset)
        {
            var longBytes = new byte[8];
            Buffer.BlockCopy(sourceBuffer, startOffset, longBytes, 0, 8);
            nextStartOffset = startOffset + 8;
            return new DateTime(BitConverter.ToInt64(longBytes, 0));
        }
        #endregion

        #region DecodeBytes(解码为byte[])
        /// <summary>
        /// 从byte数组中取出byte[]，如果消息是这样的格式：[消息长度,byte 4字节][消息内容]，取出该消息返回byte[]
        /// </summary>
        /// <param name="sourceBuffer">源字节</param>
        /// <param name="startOffset">开始位置</param>
        /// <param name="nextStartOffset">下一步开始位置</param>
        /// <returns></returns>
        public static byte[] DecodeBytes(byte[] sourceBuffer, int startOffset, out int nextStartOffset)
        {
            var lengthBytes = new byte[4];
            Buffer.BlockCopy(sourceBuffer, startOffset, lengthBytes, 0, 4);
            startOffset += 4;

            var length = BitConverter.ToInt32(lengthBytes, 0);
            var dataBytes = new byte[length];
            Buffer.BlockCopy(sourceBuffer, startOffset, dataBytes, 0, length);
            startOffset += length;
            nextStartOffset = startOffset;
            return dataBytes;
        }
        #endregion

        #region Combine(合并byte[])
        /// <summary>
        /// 合并byte[]
        /// </summary>
        /// <param name="arrays">byte数组</param>
        /// <returns></returns>
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] destination = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (var data in arrays)
            {
                Buffer.BlockCopy(data, 0, destination, offset, data.Length);
                offset += data.Length;
            }
            return destination;
        }
        #endregion

        #region ToSBCCase(半角转全角)
        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="buff">字节数组</param>
        /// <returns></returns>
        public static byte[] ToSBCCase(byte[] buff)
        {
            List<byte> tempBuff=new List<byte>();
            for (int i = 0; i < buff.Length;)
            {
                if (buff[i] == 0x20)
                {
                    tempBuff.Add(0xA1);
                    tempBuff.Add(0xA1);
                    i += 1;
                }
                else if (buff[i] == 0x7E)
                {
                    tempBuff.Add(0xA1);
                    tempBuff.Add(0xAB);
                    i += 1;
                }
                else if (buff[i] > 0x80)
                {
                    tempBuff.Add(buff[i]);
                    tempBuff.Add(buff[i+1]);
                    i += 2;
                }
                else
                {
                    tempBuff.Add(0xA3);
                    tempBuff.Add((byte)(buff[i]+0x80));
                    i += 1;
                }
            }
            return tempBuff.ToArray();
        }
        #endregion

        #region SubBuffer(截取子串)
        /// <summary>
        /// 从byte[]中截取子串
        /// </summary>
        /// <param name="buff">源字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static byte[] SubBuffer(byte[] buff, int start, int length)
        {
            if (buff.Length < start + length)
            {
                throw new ArgumentOutOfRangeException("buff");
            }
            byte[] retBuff=new byte[length];
            for (int i = 0; i < length; i++)
            {
                retBuff[i] = buff[i + start];
            }
            return retBuff;
        }
        #endregion

        #region Reverse(反转数组)
        /// <summary>
        /// 反转数组，将byte[]顺序反转
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static byte[] Reverse(byte[] bytes)
        {
            int length = bytes.Length;
            byte[] retBytes=new byte[length];
            for (int i = 0; i < length; i++)
            {
                retBytes[i] = bytes[length - i - 1];
            }
            return retBytes;
        }
        #endregion

        #region SpecCharConvert(转义特殊字符)
        /// <summary>
        /// 转义特殊字符，即'~'(0x7E)
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static byte[] SpecCharConvert(byte[] bytes)
        {
            List<byte> tempBytes=new List<byte>();
            foreach (byte b in bytes)
            {
                if (b == (byte) '~')
                {
                    //转义'~'
                    tempBytes.Add(0x7D);
                    tempBytes.Add(0x5E);                                    
                }
                else if (b == 0x7D)
                {
                    //转义0x7D
                    tempBytes.Add(0x7D);
                    tempBytes.Add(0x5D);
                }
                else
                {
                    tempBytes.Add(b);
                }
            }
            return tempBytes.ToArray();
        }
        #endregion

        #region SpecCharReverse(反转义特殊字符)
        /// <summary>
        /// 反转义特殊字符，即'~'(0x7E) 
        /// 0x7D0x5E->0x7E, 0x7D0x5D->0x7D
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static byte[] SpecCharReverse(byte[] bytes)
        {
            List<byte> tempBytes = new List<byte>();
            for (int i = 0; i < bytes.Length;)
            {
                if (bytes[i] == 0x7D)
                {
                    if (bytes[i + 1] == 0x5E)
                    {
                        tempBytes.Add((byte)'~');
                    }
                    else if (bytes[i + 1] == 0x5D)
                    {
                        tempBytes.Add(0x7D);
                    }
                    else
                    {
                        throw new ArgumentException("非法数据");
                    }
                    i += 2;
                }
                else
                {
                    tempBytes.Add(bytes[i]);
                    i += 1;
                }
            }
            return tempBytes.ToArray();
        }
        #endregion

        #region BufferLookup(查找指定字节数组首次出现位置)
        /// <summary>
        /// 查找指定字节数组首次出现的位置索引
        /// </summary>
        /// <param name="srcBuff">源字节数组</param>
        /// <param name="subBuff">需要查找的内容</param>
        /// <returns></returns>
        public static int BufferLookup(byte[] srcBuff, byte[] subBuff)
        {
            return BufferLookup(srcBuff, subBuff, 0);
        }

        /// <summary>
        /// 查找指定字节数组首次出现的位置索引
        /// </summary>
        /// <param name="srcBuff">源字节数组</param>
        /// <param name="subBuff">需要查找的内容</param>
        /// <param name="start">开始索引</param>
        /// <returns></returns>
        public static int BufferLookup(byte[] srcBuff, byte[] subBuff, int start)
        {
            for (int i = start; i < srcBuff.Length - subBuff.Length + 1; i++)
            {
                for (int j = 0; j < subBuff.Length; j++)
                {
                    if (srcBuff[i + j] != subBuff[j])
                    {
                        break;
                    }
                    if (j == subBuff.Length - 1)
                    {
                        //能运行到这里 表明subBuff中的字节都已经被匹配过
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 查找指定字节数组首次出现的位置索引
        /// </summary>
        /// <param name="srcBuff">源字节数组</param>
        /// <param name="subChars">需要查找的内容</param>
        /// <returns></returns>
        public static int BufferLookup(byte[] srcBuff, string subChars)
        {
            return BufferLookup(srcBuff, subChars,0);
        }

        /// <summary>
        /// 查找指定字节数组首次出现的位置索引
        /// </summary>
        /// <param name="srcBuff">源字节数组</param>
        /// <param name="subChars">需要查找的内容</param>
        /// <param name="start">开始索引</param>
        /// <returns></returns>
        public static int BufferLookup(byte[] srcBuff, string subChars, int start)
        {
            byte[] subBuff = Encoding.ASCII.GetBytes(subChars);
            return BufferLookup(srcBuff, subBuff, start);
        }
        #endregion

        #region GetSwapBytes(获取高低位反转byte[])
        /// <summary>
        /// 获取 ushort 的高低位反转 byte[]
        /// </summary>
        /// <param name="u">数值</param>
        /// <returns></returns>
        public static byte[] GetSwapBytes(ushort u)
        {
            return Reverse(BitConverter.GetBytes(u));
        }

        /// <summary>
        /// 获取 int 的高低位反转 byte[]
        /// </summary>
        /// <param name="i">数值</param>
        /// <returns></returns>
        public static byte[] GetSwapBytes(int i)
        {
            return Reverse(BitConverter.GetBytes(i));
        }
        #endregion

        #region GetSwapped(将倒序的高低位还原为数值)
        /// <summary>
        /// 将倒序的ushort字节还原为ushort
        /// </summary>
        /// <param name="buffer">ushort字节数组</param>
        /// <param name="start">开始索引</param>
        /// <returns></returns>
        public static ushort GetSwappedUshort(byte[] buffer, int start)
        {
            byte[] temp=new byte[2];
            temp[0] = buffer[start];
            temp[1] = buffer[start + 1];
            return BitConverter.ToUInt16(Reverse(temp), 0);
        }
        /// <summary>
        /// 将倒序的uint字节还原为uint
        /// </summary>
        /// <param name="buffer">uint字节数组</param>
        /// <param name="start">开始索引</param>
        /// <returns></returns>
        public static uint GetSwappedUint(byte[] buffer, int start)
        {
            byte[] temp = SubBuffer(buffer, start, 4);
            return BitConverter.ToUInt32(Reverse(temp), 0);
        }
        #endregion

        #region ToDbDate(将日期时间格式的字符串转换到数据库使用的日期类型)
        /// <summary>
        /// 将日期时间格式的字符串转换到数据库使用的日期类型
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static byte[] ToDbDate(DateTime dateTime)
        {
            byte[] ret=new byte[15];//后面的字节没什么用，只是因为原先参考的定义使用了 15。
            ret[0] = (byte) (dateTime.Year/100 + 100);
            ret[1] = (byte)(dateTime.Year % 100 + 100);
            ret[2] = (byte)dateTime.Month;
            ret[3] = (byte)dateTime.Day;
            ret[4] = (byte)(dateTime.Hour + 1);
            ret[5] = (byte)(dateTime.Minute + 1);
            ret[6] = (byte)(dateTime.Second + 1);
            return ret;
        }
        #endregion

        #region RevertDateTime(还原成原有的日期时间格式字符串)
        /// <summary>
        /// 还原成原有的日期时间格式字符串
        /// </summary>
        /// <param name="timeBuff">时间字节数组</param>
        /// <returns></returns>
        public static DateTime RevertDateTime(byte[] timeBuff)
        {
            string timeStr = string.Empty;
            for (int i = 0; i < 7; i++)
            {
                timeStr += timeBuff[i].ToString("X2");
            }
            //原始数据：FFFF-FF-FF FF:FF:FF 每个F对应YYYY-MM-DD hh:mm:ss中的一个数字，共7个字节。比如 0x20 0x08 0x09 0x20 0x23 0x12 0x34 表示 2008-09-20 23:12:34
            timeStr = string.Format("{0}-{1}-{2} {3}:{4}:{5}", timeStr.Substring(0, 4), timeStr.Substring(4, 2),
                timeStr.Substring(6, 2), timeStr.Substring(8, 2), timeStr.Substring(10, 2), timeStr.Substring(12, 2));
            try
            {
                return Convert.ToDateTime(timeStr);
            }
            catch (Exception)
            {
                
                return new DateTime(2000,01,01);
            }
        }
        #endregion

        #region HexToBytes(十六进制转换成二进制)
        /// <summary>
        /// 十六进制转换成二进制
        /// </summary>
        /// <param name="value">十六进制字符串</param>
        /// <returns></returns>
        public static byte[] HexToBytes(string value)
        {
            int i = value.Length%2;
            if (i != 0)
            {
                throw new Exception("字符串的长度必须是偶数.");
            }
            List<byte> bytes=new List<byte>();
            for (int j = 0; j < value.Length; j += 2)
            {
                byte b = Convert.ToByte(value.Substring(j, 2), 16);
                bytes.Add(b);
            }
            return bytes.ToArray();
        }
        #endregion

        #region Compare(确定两个字节数组是否相等)
        /// <summary>
        /// 确定两个字节数组是否相等
        /// </summary>
        /// <param name="byte1">字节数组</param>
        /// <param name="byte2">字节数组</param>
        /// <returns></returns>
        public static bool Compare(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null || byte2 == null)
            {
                return false;
            }
            if (byte1.Length != byte2.Length)
            {
                return false;
            }

            bool result = true;
            for (int i = 0; i < byte1.Length; i++)
            {
                if (byte1[i] != byte2[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region Combine(合并字节数组)
        /// <summary>
        /// 合并字节数组
        /// </summary>
        /// <param name="byte1">前缀字节数组</param>
        /// <param name="byte2">后缀字节数组</param>
        /// <returns></returns>
        public static byte[] Combine(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null)
            {
                throw new ArgumentNullException("byte1");
            }
            if (byte2 == null)
            {
                throw new ArgumentNullException("byte2");
            }

            byte[] combinedBytes=new byte[byte1.Length+byte2.Length];
            Buffer.BlockCopy(byte1, 0, combinedBytes, 0, byte1.Length);
            Buffer.BlockCopy(byte2, 0, combinedBytes, 0, byte2.Length);
            return combinedBytes;
        }
        #endregion

        #region Clone(复制字节数组)
        /// <summary>
        /// 复制一个新的字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static byte[] Clone(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            byte[] copyBytes=new byte[bytes.Length];
            Buffer.BlockCopy(bytes,0,copyBytes,0,bytes.Length);
            return copyBytes;
        }
        #endregion
    }
}
