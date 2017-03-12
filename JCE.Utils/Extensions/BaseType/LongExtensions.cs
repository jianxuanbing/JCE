/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：LongExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：7538ad8b-6c42-481e-b548-e212cf755500
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 21:59:22
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 21:59:22
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
    /// long类型的扩展辅助操作类
    /// </summary>
    public static class LongExtensions
    {
        #region Times(执行n次指定操作)
        /// <summary>
        /// 执行n次指定操作，基于底层long值
        /// </summary>
        /// <param name="value">long</param>
        /// <param name="action">操作-委托</param>
        public static void Times(this long value, Action action)
        {
            for (var i = 0; i < value; i++)
            {
                action();
            }
        }
        /// <summary>
        /// 执行n次指定操作，基于底层long值
        /// </summary>
        /// <param name="value">long</param>
        /// <param name="action">操作-委托</param>
        public static void Times(this long value, Action<long> action)
        {
            for (var i = 0; i < value; i++)
            {
                action(i);
            }
        }
        #endregion

        #region IsEven(是否偶数)
        /// <summary>
        /// 是否偶数
        /// </summary>
        /// <param name="value">long</param>
        /// <returns>bool</returns>
        public static bool IsEven(this long value)
        {
            return value % 2 == 0;
        }
        #endregion

        #region IsOdd(是否奇数)
        /// <summary>
        /// 是否奇数
        /// </summary>
        /// <param name="value">long</param>
        /// <returns>bool</returns>
        public static bool IsOdd(this long value)
        {
            return value % 2 != 0;
        }
        #endregion

        #region InRange(判断值是否在指定范围内)
        /// <summary>
        /// 判断值是否在指定范围内
        /// </summary>
        /// <param name="value">long</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this long value, long minValue, long maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }
        /// <summary>
        /// 判断值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">long</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>long</returns>
        public static long InRange(this long value, long minValue, long maxValue, long defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }
        #endregion

        #region IsPrime(是否质数)
        /// <summary>
        /// 是否质数（素数），一个质数（或素数）是具有两个不同约束的自然数：1和它本身
        /// </summary>
        /// <param name="value">long</param>
        /// <returns>bool</returns>
        public static bool IsPrime(this long value)
        {
            if ((value & 1) == 0)
            {
                if (value == 2)
                {
                    return true;
                }
                return false;
            }
            for (long i = 3; (i * i) <= value; i += 2)
            {
                if ((value % i) == 0)
                {
                    return false;
                }
            }
            return value != 1;
        }
        #endregion

        #region ToOrdinal(数值转换为顺序序号)
        /// <summary>
        /// 将数值转换为顺序序号，（英语序号）
        /// </summary>
        /// <param name="i">long</param>
        /// <returns>返回的字符串包含序号标记毗邻的数字表示</returns>
        public static string ToOrdinal(this long i)
        {
            string suffix = "th";
            switch (i % 100)
            {
                case 11:
                case 12:
                case 13:
                    break;
                default:
                    switch (i % 10)
                    {
                        case 1:
                            suffix = "st";
                            break;
                        case 2:
                            suffix = "nd";
                            break;
                        case 3:
                            suffix = "rd";
                            break;
                    }
                    break;
            }
            return string.Format("{0}{1}", i, suffix);
        }
        /// <summary>
        /// 将数值转换为指定格式的序号字符串，（英语序号）
        /// </summary>
        /// <param name="i">long</param>
        /// <param name="format">自定义格式</param>
        /// <returns>返回的字符串包含序号标记毗邻的数字表示</returns>
        public static string ToOrdinal(this long i, string format)
        {
            return string.Format(format, i.ToOrdinal());
        }
        #endregion

        #region Days(获取日期间隔)
        /// <summary>
        /// 获取日期间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="days">long</param>
        /// <returns>日期间隔</returns>
        public static TimeSpan Days(this long days)
        {
            return TimeSpan.FromDays(days);
        }
        #endregion

        #region Hours(获取小时间隔)
        /// <summary>
        /// 获取小时间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="hours">long</param>
        /// <returns>小时间隔</returns>
        public static TimeSpan Hours(this long hours)
        {
            return TimeSpan.FromHours(hours);
        }
        #endregion

        #region Minutes(获取分钟间隔)
        /// <summary>
        /// 获取分钟间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="minutes">long</param>
        /// <returns>分钟间隔</returns>
        public static TimeSpan Minutes(this long minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }
        #endregion

        #region Seconds(获取秒间隔)
        /// <summary>
        /// 获取秒间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="seconds">long</param>
        /// <returns>秒间隔</returns>
        public static TimeSpan Seconds(this long seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }
        #endregion

        #region Milliseconds(获取毫秒间隔)
        /// <summary>
        /// 获取毫秒间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="milliseconds">long</param>
        /// <returns>毫秒间隔</returns>
        public static TimeSpan Milliseconds(this long milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }
        #endregion

        #region Ticks(获取刻度间隔)
        /// <summary>
        /// 获取刻度间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="ticks">long</param>
        /// <returns>刻度间隔</returns>
        public static TimeSpan Ticks(this long ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }
        #endregion

        #region ToDateTime(将给定Unix时间戳转换为DateTime时间)
        /// <summary>
        /// 将给定 Unix 时间戳 转换为 DateTime 时间。
        /// </summary>
        /// <param name="unixTimeStamp">Unix 时间戳。</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            long value = (unixTimeStamp + 8*60*60)*10000000;
            return DateTimeExtensions.Date1970.AddTicks(value);
        }
        #endregion

    }
}
