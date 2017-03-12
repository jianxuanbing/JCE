/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DateTimeExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：307abea0-3413-4f85-87ab-5644d668a6c4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:43:21
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:43:21
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 时间（DateTime）扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        #region 属性
        /// <summary>
        /// 晚上结束时间
        /// </summary>
        const int EveningEnds = 2;
        /// <summary>
        /// 早上结束时间
        /// </summary>
        const int MorningEnds = 12;
        /// <summary>
        /// 下午结束时间
        /// </summary>
        const int AfternoonEnds = 6;
        /// <summary>
        /// 1970年1月1日
        /// </summary>
        internal static readonly DateTime Date1970 = new DateTime(1970, 1, 1);
        #endregion

        #region IsWeekend(是否周末)
        /// <summary>
        /// 判断当前时间是否周末，星期六至星期日
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns>结果</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            DayOfWeek[] weeks =
            {
                DayOfWeek.Saturday, DayOfWeek.Sunday
            };
            return weeks.Contains(dateTime.DayOfWeek);
        }
        #endregion

        #region IsWorkday(是否工作日)
        /// <summary>
        /// 判断当前时间是否工作日，星期一至星期五
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns>结果</returns>
        public static bool IsWorkday(this DateTime dateTime)
        {
            DayOfWeek[] works =
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday
            };
            return works.Contains(dateTime.DayOfWeek);
        }
        #endregion

        #region ToDateTimeString(yyyy-MM-dd HH:mm:ss)
        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            if (isRemoveSecond)
                return dateTime.ToString("yyyy-MM-dd HH:mm");
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            if (dateTime == null)
                return string.Empty;
            return ToDateTimeString(dateTime.Value, isRemoveSecond);
        }
        #endregion

        #region ToDateString(yyyy-MM-dd)
        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToDateString(dateTime.Value);
        }
        #endregion

        #region ToTimeString(HH:mm:ss)
        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToTimeString(dateTime.Value);
        }
        #endregion

        #region ToMillisecondString(yyyy-MM-dd HH:mm:ss.fff)
        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToMillisecondString(dateTime.Value);
        }
        #endregion

        #region ToChineseDateString(yyyy年MM月dd日)
        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime dateTime)
        {
            return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToChineseDateString((dateTime ?? default(DateTime)));
        }
        #endregion

        #region ToChineseDateTimeString(yyyy年MM月dd日 HH时mm分)
        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
            result.AppendFormat(" {0}时{1}分", dateTime.Hour, dateTime.Minute);
            if (isRemoveSecond == false)
                result.AppendFormat("{0}秒", dateTime.Second);
            return result.ToString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            if (dateTime == null)
                return string.Empty;
            return ToChineseDateTimeString(dateTime.Value);
        }
        #endregion

        #region UtcOffset(返回系统UTC偏移量)
        /// <summary>
        /// 返回系统UTC偏移量
        /// </summary>
        public static double UtcOffset
        {
            get { return DateTime.Now.Subtract(DateTime.UtcNow).TotalHours; }
        }
        #endregion

        #region CalculateAge(计算年龄)
        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <returns>年龄</returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth, DateTime.Now.Date);
        }
        /// <summary>
        /// 计算年龄，指定参考日期
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="referenceDate">参考日期</param>
        /// <returns>年龄</returns>
        public static int CalculateAge(this DateTime dateOfBirth, DateTime referenceDate)
        {
            var years = referenceDate.Year - dateOfBirth.Year;
            if (referenceDate.Month < dateOfBirth.Month ||
                (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day))
            {
                --years;
            }
            return years;
        }
        #endregion

        #region GetCountDaysOfMonth(获取月总天数)
        /// <summary>
        /// 获取月总天数
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>月总天数</returns>
        public static int GetCountDaysOfMonth(this DateTime date)
        {
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }
        #endregion

        #region GetFirstDayOfMonth(获取指定日期的月份第一天)
        /// <summary>
        /// 获取指定日期的月份第一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>月份第一天</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        /// <summary>
        /// 获取指定日期的月份第一天，指定星期几
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>月份第一天</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(1);
            return dt;
        }
        #endregion

        #region GetLastDayOfMonth(获取指定日期的月份最后一天)
        /// <summary>
        /// 获取指定日期的月份最后一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>最后一天</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, GetCountDaysOfMonth(date));
        }
        /// <summary>
        /// 获取指定日期的月份最后一天，指定星期几
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>最后一天</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }
        #endregion

        #region IsToday(是否今天)
        /// <summary>
        /// 判断当前指定时间是否今天
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>bool</returns>
        public static bool IsToday(this DateTime dt)
        {
            return (dt.Date == DateTime.Today);
        }
        /// <summary>
        /// 判断当前指定时间点是否今天
        /// </summary>
        /// <param name="dto">时间点</param>
        /// <returns>bool</returns>
        public static bool IsToday(this DateTimeOffset dto)
        {
            return dto.Date.IsToday();
        }
        #endregion

        #region SetTime(设置时间)
        /// <summary>
        /// 设置时间，设置时分秒
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="hours">小时</param>
        /// <param name="minutes">分钟</param>
        /// <param name="seconds">秒</param>
        /// <returns>返回设置后的时间</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hours, minutes, seconds));
        }
        /// <summary>
        /// 设置时间，设置时分秒毫秒
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="hours">小时</param>
        /// <param name="minutes">分钟</param>
        /// <param name="seconds">秒</param>
        /// <param name="milliseconds">毫秒</param>
        /// <returns>返回设置后的时间</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds, int milliseconds)
        {
            return date.SetTime(new TimeSpan(0, hours, minutes, seconds, milliseconds));
        }
        /// <summary>
        /// 设置时间，设置时间间隔
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="time">时间间隔</param>
        /// <returns>返回设置后的时间</returns>
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }
        /// <summary>
        /// 设置时间点，设置时分秒
        /// </summary>
        /// <param name="date">时间点</param>
        /// <param name="hours">小时</param>
        /// <param name="minutes">分钟</param>
        /// <param name="seconds">秒</param>
        /// <returns>返回设置后的时间点</returns>
        public static DateTimeOffset SetTime(this DateTimeOffset date, int hours, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hours, minutes, seconds));
        }
        /// <summary>
        /// 设置时间点，设置时间间隔
        /// </summary>
        /// <param name="date">时间点</param>
        /// <param name="time">时间间隔</param>
        /// <returns>返回设置后的时间点</returns>
        public static DateTimeOffset SetTime(this DateTimeOffset date, TimeSpan time)
        {
            return date.SetTime(time, null);
        }
        /// <summary>
        /// 设置时间点，设置时间间隔，时区
        /// </summary>
        /// <param name="date">时间点</param>
        /// <param name="time">时间间隔</param>
        /// <param name="localTimeZone">时区</param>
        /// <returns>返回设置后的时间点</returns>
        public static DateTimeOffset SetTime(this DateTimeOffset date, TimeSpan time, TimeZoneInfo localTimeZone)
        {
            var localDate = date.ToLocalDateTime(localTimeZone);
            localDate.SetTime(time);
            return localDate.ToDateTimeOffset(localTimeZone);
        }
        #endregion

        #region ToLocalDateTime(将时间点转换成日期时间)
        /// <summary>
        /// 将时间点转换成本地系统时区的日期时间
        /// </summary>
        /// <param name="dateTimeUtc">本地时间点</param>
        /// <returns>本地系统时区的日期时间</returns>
        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeUtc)
        {
            return dateTimeUtc.ToLocalDateTime(null);
        }
        /// <summary>
        /// 将时间点转换成指定时区的日期时间
        /// </summary>
        /// <param name="dateTimeUtc">本地时间点</param>
        /// <param name="localTimeZone">时区</param>
        /// <returns>指定时区的日期时间</returns>
        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeUtc, TimeZoneInfo localTimeZone)
        {
            localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);

            return TimeZoneInfo.ConvertTime(dateTimeUtc, localTimeZone).DateTime;
        }
        #endregion

        #region ToDateTimeOffset(将日期时间转换成时间点)
        /// <summary>
        /// 将日期时间转换成本地系统时区的时间点
        /// </summary>
        /// <param name="localDateTime">本地日期时间</param>
        /// <returns>本地系统时区的时间点</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime)
        {
            return localDateTime.ToDateTimeOffset(null);
        }
        /// <summary>
        /// 将日期时间转换成使用指定时区的时间点
        /// </summary>
        /// <param name="localDateTime">本地日期时间</param>
        /// <param name="localTimeZone">时区</param>
        /// <returns>指定时区的时间点</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime, TimeZoneInfo localTimeZone)
        {
            localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);

            if (localDateTime.Kind != DateTimeKind.Unspecified)
                localDateTime = new DateTime(localDateTime.Ticks, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);
        }
        #endregion

        #region GetFirstDayOfWeek(获取当前时间所在周的第一天)
        /// <summary>
        /// 获取当前区域性的时间所在周的第一天
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <returns>当前时间所在周的第一天</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            return date.GetFirstDayOfWeek(Const.DefaultCultureInfo);
        }
        /// <summary>
        /// 获取指定区域性的时间所在周的第一天
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="cultureInfo">指定区域性</param>
        /// <returns>当前时间所在周的第一天</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            cultureInfo = (cultureInfo ?? CultureInfo.CurrentCulture);

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (date.DayOfWeek != firstDayOfWeek)
                date = date.AddDays(-1);

            return date;
        }
        #endregion

        #region GetLastDayOfWeek(获取当前时间所在周的最后一天)
        /// <summary>
        /// 获取当前区域性的时间所在周的最后一天
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <returns>当前时间所在周的最后一天</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date)
        {
            return date.GetLastDayOfWeek(Const.DefaultCultureInfo);
        }
        /// <summary>
        /// 获取指定区域性的时间所在周的最后一天
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="cultureInfo">指定区域性</param>
        /// <returns>当前时间所在周的最后一天</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return date.GetFirstDayOfWeek(cultureInfo).AddDays(6);
        }
        #endregion

        #region GetWeeksWeekday(获取区域性的星期下一个匹配项的时间点)
        /// <summary>
        /// 获取指定周工作日当前区域性的星期内下一个匹配项的时间点
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="weekday">指定星期</param>
        /// <returns>星期下一个匹配项的时间点</returns>
        public static DateTime GetWeeksWeekday(this DateTime date, DayOfWeek weekday)
        {
            return date.GetWeeksWeekday(weekday, Const.DefaultCultureInfo);
        }
        /// <summary>
        /// 获取指定周工作日指定区域的星期内下一个匹配项的时间点
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="weekday">指定星期</param>
        /// <param name="cultureInfo">指定区域性</param>
        /// <returns>星期下一个匹配项的时间点</returns>
        public static DateTime GetWeeksWeekday(this DateTime date, DayOfWeek weekday, CultureInfo cultureInfo)
        {
            var firstDayOfWeek = date.GetFirstDayOfWeek(cultureInfo);
            return firstDayOfWeek.GetNextWeekday(weekday);
        }
        #endregion

        #region GetNextWeekday(获取指定工作日的下一个匹配项时间)
        /// <summary>
        /// 获取指定工作日的下一个匹配项时间
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="weekday">指定工作日，星期几</param>
        /// <returns>下一个工作日的时间</returns>
        public static DateTime GetNextWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(1);
            return date;
        }
        #endregion

        #region GetPreviousWeekday(获取指定工作日的上一个匹配项时间)
        /// <summary>
        /// 获取指定工作日的上一个匹配项时间
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="weekday">指定工作日，星期几</param>
        /// <returns>上一个工作日的时间</returns>
        public static DateTime GetPreviousWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(-1);
            return date;
        }

        #endregion

        #region IsDateEqual(时间中日期部分是否相等)
        /// <summary>
        /// 时间中日期部分是否相等
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="dateToCompare">匹配日期</param>
        /// <returns>bool</returns>
        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return (date.Date == dateToCompare.Date);
        }
        #endregion

        #region IsTimeEqual(时间中时间部分是否相等)
        /// <summary>
        /// 时间中时间部分是否相等
        /// </summary>
        /// <param name="time">当前时间</param>
        /// <param name="timeToCompare">匹配时间</param>
        /// <returns>bool</returns>
        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return (time.TimeOfDay == timeToCompare.TimeOfDay);
        }
        #endregion

        #region GetMillisecondsSince1970(获取当前毫秒数)
        /// <summary>
        /// 获取当前毫秒数，毫秒数=1970年1月1日-当前时间，UNIX
        /// </summary>
        /// <param name="datetime">当前时间</param>
        /// <returns>毫秒数</returns>
        public static long GetMillisecondsSince1970(this DateTime datetime)
        {
            var ts = datetime.Subtract(Date1970);
            return (long)ts.TotalMilliseconds;
        }
        #endregion

        #region AddWeeks(添加指定数量周)
        /// <summary>
        /// 添加指定数量的周到当前时间
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <param name="value">几周</param>
        /// <returns>N周后的时间</returns>
        public static DateTime AddWeeks(this DateTime date, int value)
        {
            return date.AddDays(value * 7);
        }
        #endregion

        #region GetDays(获取一年总天数)
        /// <summary>
        /// 获取指定年的总天数
        /// </summary>
        /// <param name="year">指定年</param>
        /// <returns>指定年的总天数</returns>
        public static int GetDays(int year)
        {
            return GetDays(year, Const.DefaultCultureInfo);
        }
        /// <summary>
        /// 获取指定年的总天数，使用指定区域性
        /// </summary>
        /// <param name="year">指定年</param>
        /// <param name="culture">指定区域性</param>
        /// <returns>指定年的总天数</returns>
        public static int GetDays(int year, CultureInfo culture)
        {
            var first = new DateTime(year, 1, 1, culture.Calendar);
            var last = new DateTime(year + 1, 1, 1, culture.Calendar);
            return GetDays(first, last);
        }
        /// <summary>
        /// 获取指定时间的年度总天数
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>指定时间的年度总天数</returns>
        public static int GetDays(this DateTime date)
        {
            return GetDays(date.Year, Const.DefaultCultureInfo);
        }
        /// <summary>
        /// 获取指定时间的年度总天数，使用指定区域性
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <param name="culture">指定区域性</param>
        /// <returns>指定时间的年度总天数</returns>
        public static int GetDays(this DateTime date, CultureInfo culture)
        {
            return GetDays(date.Year, culture);
        }
        /// <summary>
        /// 获取两个时间之间的天数
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns>天数</returns>
        public static int GetDays(this DateTime fromDate, DateTime toDate)
        {
            return Convert.ToInt32(toDate.Subtract(fromDate).TotalDays);
        }
        #endregion

        #region GetPeriodOfDay(获取指定时间的时间段)
        /// <summary>
        /// 获取指定时间的时间段，（早上、下午、晚上）
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>时间段</returns>
        public static string GetPeriodOfDay(this DateTime date)
        {
            var hour = date.Hour;
            if (hour < EveningEnds)
            {
                //return "evening";
                return "晚上";
            }
            if (hour < MorningEnds)
            {
                //return "morning";
                return "早上";
            }
            //return hour < AfternoonEnds ? "afternoon" : "evening";
            return hour < AfternoonEnds ? "下午" : "晚上";
        }
        #endregion

        #region GetWeekOfYear(获取指定时间在一年中所在的周索引)
        /// <summary>
        /// 获取指定时间在一年中所在的周索引
        /// </summary>
        /// <param name="dateTime">指定时间</param>
        /// <param name="culture">指定区域性</param>
        /// <returns>周索引</returns>
        public static int GetWeekOfYear(this DateTime dateTime, CultureInfo culture)
        {
            var calendar = culture.Calendar;
            var dateTimeFormat = culture.DateTimeFormat;

            return calendar.GetWeekOfYear(dateTime, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
        }
        /// <summary>
        /// 获取指定时间在一年中所在的周索引
        /// </summary>
        /// <param name="dateTime">指定时间</param>
        /// <returns>周索引</returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            return GetWeekOfYear(dateTime, Const.DefaultCultureInfo);
        }
        #endregion

        #region IsEaster(是否复活节)
        /// <summary>
        /// 是否复活节
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>bool</returns>
        public static bool IsEaster(this DateTime date)
        {
            int year = date.Year;
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int L = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * L) / 451;
            int month = (h + L - 7 * m + 114) / 31;
            int day = ((h + L - 7 * m + 114) % 31) + 1;

            DateTime dtEasterSunday = new DateTime(year, month, day);
            return date == dtEasterSunday;
        }
        #endregion

        #region IsBefore(源时间是否在目标时间之前)
        /// <summary>
        /// 源时间是否在目标时间之前
        /// </summary>
        /// <param name="source">源时间</param>
        /// <param name="other">目标时间</param>
        /// <returns>bool</returns>
        public static bool IsBefore(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) < 0;
        }
        #endregion

        #region IsAfter(源时间是否在目标时间之后)
        /// <summary>
        /// 源时间是否在目标时间之后
        /// </summary>
        /// <param name="source">源时间</param>
        /// <param name="other">目标时间</param>
        /// <returns>bool</returns>
        public static bool IsAfter(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) > 0;
        }
        #endregion

        #region Tomorrow(获取第二天时间)
        /// <summary>
        /// 获取第二天的时间，明天
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>明天</returns>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }
        #endregion

        #region Yesterday(获取前一天的时间)
        /// <summary>
        /// 获取前一天的时间，昨天
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>昨天</returns>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }
        #endregion

        #region ToFriendlyDateString(将指定时间转换成友好的时间字符串表示方式)
        /// <summary>
        /// 将指定时间转换成友好的时间字符串表示方式，（昨天 12:30下午，今天 3:33下午）
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <param name="culture">指定区域性</param>
        /// <returns>友好的时间字符串表示方式</returns>
        public static string ToFriendlyDateString(this DateTime date, CultureInfo culture)
        {
            var sbFormattedDate = new StringBuilder();
            if (date.Date == DateTime.Today)
            {
                //sbFormattedDate.Append("Today");
                sbFormattedDate.Append("今天");
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                //sbFormattedDate.Append("Yesterday");
                sbFormattedDate.Append("昨天");
            }
            else if (date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                sbFormattedDate.Append(date.ToString("dddd").ToString(culture));
            }
            else
            {
                sbFormattedDate.Append(date.ToString("MMMM dd, yyyy").ToString(culture));
            }

            //append the time portion to the output
            //sbFormattedDate.Append(" at ").Append(date.ToString("t").ToLower());
            sbFormattedDate.Append(date.ToString("t").ToLower());
            return sbFormattedDate.ToString();
        }
        /// <summary>
        /// 将指定时间转换成友好的时间字符串表示方式，（昨天 12:30下午，今天 3:33下午）
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>友好的时间字符串表示方式</returns>
        public static string ToFriendlyDateString(this DateTime date)
        {
            return ToFriendlyDateString(date, Const.DefaultCultureInfo);
        }
        #endregion

        #region EndOfDay(设置指定时间为当天的最后的时间)
        /// <summary>
        /// 设置指定时间为当天的最后的时间，23:59:59.999
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>当天的最后的时间</returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.SetTime(23, 59, 59, 999);
        }
        #endregion

        #region Noon(设置指定时间为中午)
        /// <summary>
        /// 设置指定时间为中午,12:00:00
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns>中午</returns>
        public static DateTime Noon(this DateTime time)
        {
            return time.SetTime(12, 0, 0);
        }
        #endregion

        #region Midnight(设置指定时间为午夜)
        /// <summary>
        /// 设置指定时间为午夜（凌晨）,00:00:00
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns>午夜</returns>
        public static DateTime Midnight(this DateTime time)
        {
            return time.SetTime(0, 0, 0, 0);
        }
        #endregion

        #region MultiplyBy(时间间隔乘)
        /// <summary>
        /// 时间间隔乘
        /// </summary>
        /// <param name="source">时间间隔</param>
        /// <param name="factor">系数</param>
        /// <returns>相乘后的时间间隔</returns>
        public static TimeSpan MultiplyBy(this TimeSpan source, int factor)
        {
            TimeSpan result = TimeSpan.FromTicks(source.Ticks * factor);
            return result;
        }
        /// <summary>
        /// 时间间隔乘，时间间隔 * 系数
        /// </summary>
        /// <param name="source">时间间隔</param>
        /// <param name="factor">系数</param>
        /// <returns>相乘后的时间间隔</returns>
        public static TimeSpan MultiplyBy(this TimeSpan source, double factor)
        {
            TimeSpan result = TimeSpan.FromTicks((long)(source.Ticks * factor));
            return result;
        }
        #endregion

        #region DayOfQuarter(获取指定日期所在季度的第一天/最后一天)
        /// <summary>
        /// 获取指定日期所在季度的第一天/最后一天
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="firstDay">是否第一天,true:是,false:否</param>
        /// <returns></returns>
        public static DateTime DayOfQuarter(this DateTime time, bool firstDay)
        {
            int month = 0;
            switch (time.Month)
            {
                case 1:
                case 2:
                case 3:
                    month = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    month = 4;
                    break;
                case 7:
                case 8:
                case 9:
                    month = 7;
                    break;
                case 10:
                case 11:
                case 12:
                    month = 10;
                    break;
                
            }
            DateTime result=new DateTime(time.Year,month,1);
            if (firstDay)
            {
                return result;
            }
            return result.AddMonths(3).AddDays(-1);
        }
        #endregion

        #region DayOfYear(获取指定日期所在年的第一天/最后一天)
        /// <summary>
        /// 获取指定日期所在年的第一天/最后一天
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="firstDay">是否第一天,true:是,false:否</param>
        /// <returns></returns>
        public static DateTime DayOfYear(this DateTime time, bool firstDay)
        {
            if (firstDay)
            {
                return new DateTime(time.Year,1,1);
            }
            return new DateTime(time.Year,12,31);
        }
        #endregion

        #region ToUnixTimeStamp(将给定DateTime时间转换为Unix时间戳)
        /// <summary>
        /// 将给定 DateTime 时间转换为 Unix 时间戳
        /// </summary>
        /// <param name="dateTime">DateTime 时间。</param>
        /// <returns>Unix 时间戳。</returns>
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return (dateTime.Ticks - Date1970.Ticks)/10000000 - 8*60*60;
        }
        #endregion
    }
}
