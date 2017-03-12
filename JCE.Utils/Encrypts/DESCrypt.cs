/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Encrypts
 * 文件名：DESCrypt
 * 版本号：v1.0.0.0
 * 唯一标识：a9715c6a-fb5b-4fad-bc0a-126631510f2c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/11 9:02:48
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/11 9:02:48
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// DES（Data Encryption Standard）算法，
    /// 是一种对称加密算法(破解方式：穷举法)
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class DESCrypt
    {
        #region Encrypt(加密)
        /// <summary>
        /// 指定密匙对明文进行DES加密
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        public static string Encrypt(string text, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(text);
            des.Key =
                ASCIIEncoding.ASCII.GetBytes(
                    System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                        .Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(
                    System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                        .Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }
        #endregion

        #region Decrypt(解密)
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">密文</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        public static string Decrypt(string text, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key =
                ASCIIEncoding.ASCII.GetBytes(
                    System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                        .Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(
                    System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                        .Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
