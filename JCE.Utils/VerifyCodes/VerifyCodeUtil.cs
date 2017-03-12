/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.VerifyCodes
 * 文件名：VerifyCodeUtil
 * 版本号：v1.0.0.0
 * 唯一标识：77ec00b6-b3b5-4dd7-b89d-c78ac048e146
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:17:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:17:47
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

namespace JCE.Utils.VerifyCodes
{
    /// <summary>
    /// 验证码操作工具类
    /// </summary>
    public class VerifyCodeUtil
    {
        #region 变量
        //验证码的最小和最大的长度
        private const int CodeMinLength = 4;
        private const int CodeMaxLength = 10;

        //验证码字典
        private static readonly char[] ArrNumberCode = Const.ArabicNumbers.ToCharArray();
        private static readonly char[] ArrLowLetterCode = Const.Lowercase.ToCharArray();
        private static readonly char[] ArrHighLetterCode = Const.Uppercase.ToCharArray();
        #endregion

        #region Instance(验证码生成帮助类实例)
        /// <summary>
        /// 验证码生成帮助类实例
        /// </summary>
        /// <returns></returns>
        public static VerifyCodeUtil Instance
        {
            get
            {
                return new VerifyCodeUtil();
            }
        }
        #endregion

        #region GetLength(判断长度是否合适)
        /// <summary>
        /// 判断长度是否合适，如何不合适，则以最大和最小长度进行约束
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        private int GetLength(int length)
        {
            if (length < CodeMinLength)
            {
                length = CodeMinLength;
            }
            if (length > CodeMaxLength)
            {
                length = CodeMaxLength;
            }
            return length;
        }
        #endregion

        #region GetRandomSeed(生成随机数的种子)
        /// <summary>
        /// 生成随机数的种子 
        /// </summary>
        /// <returns></returns>
        public int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        #endregion

        #region GetOnlyNumber(生成纯数字的随机验证码)
        /// <summary>
        /// 生成纯数字的随机验证码，根据指定长度
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string GetOnlyNumber(int length)
        {
            StringBuilder sb = new StringBuilder();
            length = GetLength(length);
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < length; i++)
            {
                sb.Append(ArrNumberCode[random.Next(ArrNumberCode.Length)]);
            }
            return sb.ToString();
        }
        #endregion

        #region GetOnlyLowLetters(获取纯小写字母的验证码)
        /// <summary>
        /// 获取纯小写字母的验证码，根据指定长度
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string GetOnlyLowLetters(int length)
        {
            StringBuilder sb = new StringBuilder();
            length = GetLength(length);
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < length; i++)
            {
                sb.Append(ArrLowLetterCode[random.Next(ArrLowLetterCode.Length)]);
            }
            return sb.ToString();
        }
        #endregion

        #region GetNumberLow(获取数字+小写字母验证码)
        /// <summary>
        /// 获取数字+小写字母验证码，根据指定长度
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string GetNumberLow(int length)
        {
            StringBuilder sb = new StringBuilder();
            length = GetLength(length);
            Random random = new Random(GetRandomSeed());
            ;
            List<char[]> numberLowList = new List<char[]>
            {
                ArrNumberCode,
                ArrLowLetterCode
            };
            for (int i = 0; i < length; i++)
            {
                char[] randomChar = numberLowList[random.Next(numberLowList.Count)];
                sb.Append(randomChar[random.Next(randomChar.Length)]);
            }
            return sb.ToString();
        }
        #endregion

        #region GetAllFixd(生成大小写字母+数字的随机验证码)
        /// <summary>
        /// 生成大小写字母+数字的随机验证码，根据指定长度
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string GetAllFixd(int length)
        {
            StringBuilder sb = new StringBuilder();
            length = GetLength(length);
            Random random = new Random(GetRandomSeed());
            ;
            List<char[]> numberLowList = new List<char[]>
            {
                ArrNumberCode,
                ArrLowLetterCode,
                ArrHighLetterCode
            };
            for (int i = 0; i < length; i++)
            {
                char[] randomChar = numberLowList[random.Next(numberLowList.Count)];
                sb.Append(randomChar[random.Next(randomChar.Length)]);
            }
            return sb.ToString();
        }
        #endregion
    }
}
