/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：RandomExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：166618e0-5837-4c5b-91bd-3a6d87e89dbb
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:53:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:53:35
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

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 随机数（Random）类型扩展
    /// </summary>
    public static class RandomExtensions
    {
        #region NextBoolean(返回随机布尔值)
        /// <summary>
        /// 返回随机布尔值
        /// </summary>
        /// <param name="random">Random</param>
        /// <returns>随机布尔值</returns>
        public static bool NextBoolean(this Random random)
        {            
            return random.NextDouble() > 0.5;
        }
        #endregion

        #region NextEnum(返回指定枚举类型的随机枚举值)
        /// <summary>
        /// 返回指定枚举类型的随机枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="random">返回随机布尔值</param>
        /// <returns>指定枚举类型的随机枚举值</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }
            Array array = Enum.GetValues(type);
            int index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }
        #endregion

        #region NextBytes(返回随机数填充的指定长度的数组)
        /// <summary>
        /// 返回随机数填充的指定长度的byte[]数组
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="length">数组长度</param>
        /// <returns>随机数填充的指定长度的byte[]数组</returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            byte[] data = new byte[length];
            random.NextBytes(data);
            return data;
        }
        #endregion

        #region NextItem(返回数组中的随机元素)
        /// <summary>
        /// 返回数组中的随机元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="random">Random</param>
        /// <param name="items">元素数组</param>
        /// <returns>元素数组中的某个随机项</returns>
        public static T NextItem<T>(this Random random, T[] items)
        {
            return items[random.Next(0, items.Length)];
        }
        #endregion

        #region NextDateTime(返回随机时间值)
        /// <summary>
        /// 返回随机时间值
        /// </summary>
        /// <param name="random">Random</param>
        /// <returns>随机时间值</returns>
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }
        /// <summary>
        /// 返回指定时间段内的随机时间值
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="minValue">时间范围的最小值</param>
        /// <param name="maxValue">时间范围的最大值</param>
        /// <returns>指定时间段内的随机时间值</returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            long ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }
        #endregion

        #region GetRandomNumberString(获取指定的长度的随机数字字符串)
        /// <summary>
        /// 获取指定的长度的随机数字字符串
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机数字符串</returns>
        public static string GetRandomNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            char[] pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string result = "";
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

        #region GetRandomLetterString(获取指定的长度的随机字母字符串)
        /// <summary>
        /// 获取指定的长度的随机字母字符串
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母组成字符串</returns>
        public static string GetRandomLetterString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            char[] pattern = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

        #region GetRandomLetterAndNumberString(获取指定的长度的随机字母和数字字符串)
        /// <summary>
        /// 获取指定的长度的随机字母和数字字符串
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母和数字组成字符串</returns>
        public static string GetRandomLetterAndNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            char[] pattern = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion
    }
}
