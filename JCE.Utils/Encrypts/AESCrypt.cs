/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Encrypts
 * 文件名：AESCrypt
 * 版本号：v1.0.0.0
 * 唯一标识：ffc4fb0d-0e6a-4e48-b801-37649a138ba8
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/11 9:02:16
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/11 9:02:16
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

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// AES（Advanced Encryption Standard）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class AESCrypt
    {
        #region Fields(字段)
        /// <summary>
        /// 返回错误码
        /// </summary>
        public const string RET_ERROR = "x07x07x07x07x07";

        /// <summary>
        /// 对称算法初始化向量
        /// </summary>
        private readonly byte[] _iv =
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD,
            0xEF
        };
        /// <summary>
        /// 加密或解密密匙
        /// </summary>
        private byte[] _Key =
        {
            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab,
            0x76, 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, 0xb7,
            0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7, 0x23,
            0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b,
            0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1,
            0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45,
            0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda,
            0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64,
            0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b,
            0xdb, 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7,
            0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, 0xba, 0x78, 0x25,
            0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e, 0xb5, 0x66, 0x48,
            0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e,
            0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41,
            0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16
        };
        /// <summary>
        /// 加(解)密密匙
        /// </summary>
        private const string CRYPTO_KEY = "ADVANCEDENCRYPTIONSTANDARD";
        /// <summary>
        /// 加密密匙长度
        /// </summary>
        private int CRYPTO_KEY_LENGTH = 32;
        /// <summary>
        /// AES加密算法接口
        /// </summary>
        private AesCryptoServiceProvider _aesCryptoServiceProvider;
        #endregion

        #region Property(属性)
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 密文中是否包含密匙
        /// </summary>
        public bool ContainKey { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 构造函数，默认
        /// </summary>
        public AESCrypt()
        {
            _aesCryptoServiceProvider = new AesCryptoServiceProvider();
            ContainKey = true;
            Message = string.Empty;
        }
        /// <summary>
        /// 构造函数，设置密文中是否包含密匙
        /// </summary>
        /// <param name="containKey">密文中是否包含密匙</param>
        public AESCrypt(bool containKey) : this()
        {
            ContainKey = containKey;
        }
        #endregion

        #region Encrypt(加密)
        /// <summary>
        /// 指定密匙对明文进行AES加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="keyStr">加密密匙</param>
        /// <returns></returns>
        public string Encrypt(string plaintext, string keyStr)
        {
            byte[] key = new byte[CRYPTO_KEY_LENGTH];
            byte[] temp = String2Byte(keyStr);
            if (temp.Length > key.Length)
            {
                Message = "Key too long,need less than 32 Bytes key.";
                return RET_ERROR;
            }
            key = String2Byte(keyStr.PadRight(key.Length));
            return Encrypt(plaintext, key);
        }
        /// <summary>
        /// 动态生成密匙，并对明文进行AES加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public string Encrypt(string plaintext)
        {
            byte[] key = new byte[CRYPTO_KEY_LENGTH];
            _aesCryptoServiceProvider.GenerateKey();
            key = _aesCryptoServiceProvider.Key;
            return Encrypt(plaintext, key);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt">需要加密的文本</param>
        /// <param name="key">密匙</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, Encoding encoding)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key","key值不能为空");
            }
            if (string.IsNullOrEmpty(toEncrypt))
            {
                return toEncrypt;
            }
            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }
            try
            {
                byte[] keyArray = encoding.GetBytes(key);
                byte[] toEncryptArray = encoding.GetBytes(toEncrypt);
                var resultArray = Encrypt(keyArray, toEncryptArray);
                result = Convert.ToBase64String(resultArray);
            }
            catch (Exception)
            {
                
            }
            return result;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="keyArray">密匙</param>
        /// <param name="toEncryptArray">需要加密的文本</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">密匙大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] keyArray, byte[] toEncryptArray, byte[] iv = null, int keySize = 256,
            int blockSize = 128, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes=Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Key = keyArray;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;

                ICryptoTransform cTransform = aes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
        }
        #endregion

        #region Decrypt(解密)
        /// <summary>
        /// 指定密匙并对密文进行AES解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="keyStr">密匙</param>
        /// <returns></returns>
        public string Decrypt(string encrypted, string keyStr)
        {
            byte[] key = new byte[CRYPTO_KEY_LENGTH];
            byte[] temp = String2Byte(keyStr);
            if (temp.Length > key.Length)
            {
                Message = "Key invalid.too long,need less than 32 Bytes";
                return RET_ERROR;
            }
            key = String2Byte(keyStr.PadRight(key.Length));
            if (ContainKey)
            {
                encrypted = encrypted.Substring(CRYPTO_KEY_LENGTH * 2);
            }
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// 从密文中解析出密匙，并对密文进行AES解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns></returns>
        public string Decrypt(string encrypted)
        {
            string keyStr = string.Empty;
            byte[] key = new byte[CRYPTO_KEY_LENGTH];
            if (encrypted.Length <= CRYPTO_KEY_LENGTH * 2)
            {
                Message = "Encrypted string invalid.";
                return RET_ERROR;
            }
            if (ContainKey)
            {
                keyStr = encrypted.Substring(0, CRYPTO_KEY_LENGTH * 2);
                encrypted = encrypted.Substring(CRYPTO_KEY_LENGTH * 2);
            }
            key = HexString2Byte(keyStr);
            return Decrypt(encrypted, key);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt">需要解密的字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, Encoding encoding)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "key值不能为空");
            }
            if (string.IsNullOrEmpty(toDecrypt))
            {
                return toDecrypt;
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            try
            {
                byte[] keyArray = encoding.GetBytes(key);
                byte[] toDecryptArray = Convert.FromBase64String(toDecrypt);
                var resultArray = Decrypt(keyArray, toDecryptArray);
                result = encoding.GetString(resultArray);
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="keyArray">密匙</param>
        /// <param name="toDecryptArray">需要解密的文本</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">密匙大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] keyArray, byte[] toDecryptArray, byte[] iv = null, int keySize = 256,
            int blockSize = 128, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes=Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;
                aes.Key = keyArray;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                ICryptoTransform cTransform = aes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                return resultArray;
            }
        }
        #endregion

        #region Private Methods(私有方法)
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="cryptoStr">需要加密的字符串</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        private string Encrypt(string cryptoStr, byte[] key)
        {
            string encryptedStr = string.Empty;
            try
            {
                byte[] crypto = String2Byte(cryptoStr);
                _aesCryptoServiceProvider.Key = key;
                _aesCryptoServiceProvider.IV = _iv;
                ICryptoTransform ct = _aesCryptoServiceProvider.CreateEncryptor();
                byte[] encrypted = ct.TransformFinalBlock(crypto, 0, crypto.Length);
                if (ContainKey)
                {
                    encryptedStr += Byte2HexString(key);
                }
                encryptedStr += Byte2HexString(encrypted);
                return encryptedStr;
            }
            catch (Exception ex)
            {
                Message = ex.ToString() + "Encrypt fail.";
                return RET_ERROR;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedStr">需要解密的字符串</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        private string Decrypt(string encryptedStr, byte[] key)
        {
            string decryptedStr = string.Empty;
            try
            {
                byte[] encrypted = HexString2Byte(encryptedStr);
                _aesCryptoServiceProvider.Key = key;
                _aesCryptoServiceProvider.IV = _iv;
                ICryptoTransform ct = _aesCryptoServiceProvider.CreateDecryptor();
                byte[] decrypted = ct.TransformFinalBlock(encrypted, 0, encrypted.Length);
                decryptedStr += Byte2String(decrypted);
                return decryptedStr;
            }
            catch (Exception ex)
            {
                Message = ex.ToString() + "Decrypt fail.";
                return RET_ERROR;
            }
        }
        /// <summary>
        /// Byte[]转十六进制字符串
        /// </summary>
        /// <param name="bytes">需要转换的字节数组</param>
        /// <returns></returns>
        private string Byte2HexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 十六进制字符串转Byte[]
        /// </summary>
        /// <param name="hex">需要转换的字符串</param>
        /// <returns></returns>
        private byte[] HexString2Byte(string hex)
        {
            int len = hex.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16));
            }
            return bytes;
        }

        /// <summary>
        /// 字符串转Byte[]
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns></returns>
        private byte[] String2Byte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        /// <summary>
        /// Byte[]转字符串
        /// </summary>
        /// <param name="bytes">需要转换的字节数组</param>
        /// <returns></returns>
        private string Byte2String(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion
    }
}
