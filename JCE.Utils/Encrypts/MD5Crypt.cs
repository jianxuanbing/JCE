/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Encrypts
 * 文件名：MD5Crypt
 * 版本号：v1.0.0.0
 * 唯一标识：123bece7-e580-4aff-8046-8a686c0b3090
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 11:35:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 11:35:07
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// MD5（Message Digest Algorithm）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class MD5Crypt
    {
        #region EncryptBy16(16位加密)
        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string EncryptBy16(string text)
        {
            return EncryptBy16(text, Encoding.UTF8);
        }
        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string EncryptBy16(string text, Encoding encoding)
        {
            return Encrypt(text, encoding, 4, 8);
        }
        #endregion

        #region EncryptBy32(32位加密)
        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string EncryptBy32(string text)
        {
            return EncryptBy32(text, Encoding.UTF8);
        }
        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string EncryptBy32(string text, Encoding encoding)
        {
            return Encrypt(text, encoding, null, null);
        }
        #endregion

        #region Encrypt(加密)
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">加密编码方式</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string Encrypt(string text, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                if (startIndex == null)
                {
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)));
                }
                else
                {
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)), startIndex.SafeValue(),
                        length.SafeValue());
                }
            }
            finally
            {
                md5.Clear();
            }
            return result.Replace("-", "");
        }
        #endregion

    }
}
