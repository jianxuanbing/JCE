/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：FloatExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：8f14e077-00e0-4b7b-afed-51c46922f13b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 20:44:46
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 20:44:46
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
    /// float类型的扩展辅助操作类
    /// </summary>
    public static class FloatExtensions
    {
        #region InRange(判断值是否在指定范围内)
        /// <summary>
        /// 判断当前值是否在指定范围内
        /// </summary>
        /// <param name="value">float</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>bool</returns>
        public static bool InRange(this float value, float minValue, float maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }
        /// <summary>
        /// 判断值是否在指定范围内，否则返回默认值
        /// </summary>
        /// <param name="value">float</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>float</returns>
        public static float InRange(this float value, float minValue, float maxValue, float defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }
        #endregion

        #region Days(获取日期间隔)
        /// <summary>
        /// 获取日期间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="days">float</param>
        /// <returns>日期间隔</returns>
        public static TimeSpan Days(this float days)
        {
            return TimeSpan.FromDays(days);
        }
        #endregion

        #region Hours(获取小时间隔)
        /// <summary>
        /// 获取小时间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="hours">float</param>
        /// <returns>小时间隔</returns>
        public static TimeSpan Hours(this float hours)
        {
            return TimeSpan.FromHours(hours);
        }
        #endregion

        #region Minutes(获取分钟间隔)
        /// <summary>
        /// 获取分钟间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="minutes">float</param>
        /// <returns>分钟间隔</returns>
        public static TimeSpan Minutes(this float minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }
        #endregion

        #region Seconds(获取秒间隔)
        /// <summary>
        /// 获取秒间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="seconds">float</param>
        /// <returns>秒间隔</returns>
        public static TimeSpan Seconds(this float seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }
        #endregion

        #region Milliseconds(获取毫秒间隔)
        /// <summary>
        /// 获取毫秒间隔，根据数值获取时间间隔
        /// </summary>
        /// <param name="milliseconds">float</param>
        /// <returns>毫秒间隔</returns>
        public static TimeSpan Milliseconds(this float milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }
        #endregion
    }
}
