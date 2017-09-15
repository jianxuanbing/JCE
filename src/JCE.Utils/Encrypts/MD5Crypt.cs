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
                throw new ArgumentNullException("text","MD5加密的字符串不能为空!");
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

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">加密编码方式</param>
        /// <returns></returns>
        public static string Encrypt(string text, Encoding encoding = null)
        {
            return Encrypt(text, encoding ?? Encoding.UTF8, null, null);
        }
        #endregion
    }
}
