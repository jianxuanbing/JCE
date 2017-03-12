/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Encrypts
 * 文件名：SHA1Crypt
 * 版本号：v1.0.0.0
 * 唯一标识：e00c11a0-709c-4763-b929-3211477b5cea
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/11 9:05:21
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/11 9:05:21
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
    /// SHA1（Secure Hash Algorithm）算法，适用于数字签名标准
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SHA1Crypt
    {
        #region Encrypt(加密)
        /// <summary>
        /// 对指定文本进行SHA1加密
        /// </summary>
        /// <param name="text">需要加密的文本</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string Encrypt(string text, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("input", "SHA1加密的字符串不能为空!");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var data = encoding.GetBytes(text);
            var encryData = Encrypt(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryData.Length; i++)
            {
                sb.Append(encryData[i].ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 对指定文本进行SHA1加密并返回Base64编码格式字符串
        /// </summary>
        /// <param name="text">需要加密的文本</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string EncryptBase64(string text, Encoding encoding = null)
        {
            if (text.IsEmpty())
            {
                throw new ArgumentNullException("input", "SHA1加密的字符串不能为空!");
            }
            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }
            var data = encoding.GetBytes(text);
            var encryData = Encrypt(data);
            return Convert.ToBase64String(encryData);
        }
        /// <summary>
        /// 对指定字节数组进行SHA1加密
        /// </summary>
        /// <param name="bytes">需要加密的字节数组</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException("bytes", "SHA1加密的字节不能为空!");
            }
            using (SHA1 sha1Hash = SHA1.Create())
            {
                return sha1Hash.ComputeHash(bytes);
            }
        }
        #endregion

    }
}
