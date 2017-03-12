/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Randoms
 * 文件名：RandomBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：f75adc25-a7a3-409f-9310-b8e742ea8c88
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/23 22:05:42
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/23 22:05:42
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Common;

namespace JCE.Utils.Randoms
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public class RandomBuilder
    {
        #region Field(字段)
        /// <summary>
        /// 随机数操作
        /// </summary>
        private readonly Random _random;
        /// <summary>
        /// 重复数
        /// </summary>
        private int repeat = 0;
        #endregion

        #region Construct(构造函数)
        /// <summary>
        /// 初始化随机数生成器
        /// </summary>
        public RandomBuilder()
        {
            _random = new Random();
        }
        #endregion

        #region GenerateString(生成随机字符串)

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="hasUppercase">是否包含大写字母,true:是,false:否</param>
        /// <returns></returns>
        public string GenerateString(int maxLength,bool hasUppercase=false)
        {
            string text = hasUppercase
                ? Const.Lowercase + Const.Uppercase + Const.ArabicNumbers
                : Const.Lowercase + Const.ArabicNumbers;
            return Generate(maxLength, text);
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="text">随机内容</param>
        /// <returns></returns>
        public string GenerateString(int maxLength, string text)
        {
            return Generate(maxLength, text);
        }
        #endregion

        #region GenerateChinese(生成随机常用汉字)
        /// <summary>
        /// 生成随机常用汉字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public string GenerateChinese(int maxLength)
        {
            return Generate(maxLength, Const.SimplifiedChinese);
        }
        #endregion

        #region GenerateLetters(生成随机字母，不出现汉字和数字)

        /// <summary>
        /// 生成随机字母，不出现汉字和数字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public string GenerateLetters(int maxLength)
        {
            return Generate(maxLength, Const.Uppercase + Const.Lowercase);
        }

        /// <summary>
        /// 生成随机字母，不出现汉字和数字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="isUppercase">是否大写字母,true:是,false:否</param>
        /// <returns></returns>
        public string GenerateLetters(int maxLength,bool isUppercase)
        {
            return Generate(maxLength, isUppercase ? Const.Uppercase : Const.Lowercase);
        }
        #endregion

        #region GenerateBool(生成随机布尔值)
        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        /// <returns></returns>
        public bool GenerateBool()
        {
            var random = _random.GetInt(1, 3);
            if (random == 1)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region GenerateDate(生成随机日期)
        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="beginYear">起始年份</param>
        /// <param name="endYear">结束年份</param>
        /// <returns></returns>
        public DateTime GenerateDate(int beginYear = 2000, int endYear = 2030)
        {
            var year = _random.GetInt(beginYear, endYear);
            var month = _random.GetInt(1, 13);
            var day = _random.GetInt(1, 29);
            var hour = _random.GetInt(1, 24);
            var minute = _random.GetInt(1, 60);
            var second = _random.GetInt(1, 60);
            return new DateTime(year, month, day, hour, minute, second);
        }
        #endregion

        #region GenerateInt(生成随机整数)
        /// <summary>
        /// 生成随机整数
        /// </summary>
        /// <param name="maxValue">整数最大值</param>
        /// <returns></returns>
        public int GenerateInt(int maxValue)
        {
            return _random.GetInt(0, maxValue + 1);
        }
        /// <summary>
        /// 生成随机整数
        /// </summary>
        /// <param name="minValue">整数最小值</param>
        /// <param name="maxValue">整数最大值</param>
        /// <returns></returns>
        public int GenerateInt(int minValue, int maxValue)
        {
            return _random.GetInt(minValue, maxValue);
        }
        #endregion

        #region GenerateEnum(生成随机枚举)
        /// <summary>
        /// 生成随机枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns></returns>
        public T GenerateEnum<T>()
        {
            var list = EnumUtil.GetItems<T>();
            int index = _random.GetInt(0, list.Count);
            return EnumUtil.GetInstance<T>(list[index].Value);
        }
        #endregion

        #region GenerateNumber(生成随机数字)
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public string GenerateNumber(int maxLength)
        {
            StringBuilder sb=new StringBuilder();
            long ticks = DateTime.Now.Ticks + this.repeat;
            this.repeat++;
            Random random = new Random((int) ((ulong) ticks & 0xffffffffL) | (int) (ticks >> this.repeat));
            for (int i = 0; i < maxLength; i++)
            {
                int num = random.GetInt();
                string temp = ((char) (0x30 + (ushort) (num%10))).ToString();
                sb.Append(temp);
            }
            return sb.ToString();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="text">随机内容</param>
        /// <returns></returns>
        private string Generate(int maxLength, string text)
        {
            //var length = GetLength(maxLength);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < maxLength; i++)
            {
                sb.Append(GetRandomChar(text));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取随机长度
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        private int GetLength(int maxLength)
        {
            return _random.GetInt(1, maxLength);
        }

        /// <summary>
        /// 获取随机字符
        /// </summary>
        /// <param name="text">随机内容</param>
        /// <returns></returns>
        private string GetRandomChar(string text)
        {
            var index = _random.GetInt(0, text.Length);
            return text[index].ToString();
        }
        #endregion
    }
}
