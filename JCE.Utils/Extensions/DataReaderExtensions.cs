/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DataReaderExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b9cfcdfa-d161-435b-862b-6bb57552aca2
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:41:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:41:00
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 数据读取器（IDataReader）扩展
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// 获取指定字段的数据类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Get<T>(this IDataReader reader, string field, T defaultValue = default(T))
        {
            var value = reader[field];
            if (value == DBNull.Value)
            {
                return defaultValue;
            }
            if (value is T)
            {
                return (T)value;
            }
            return value.ConvertTo(defaultValue);
        }
        /// <summary>
        /// 获取指定字段的字节数组
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static byte[] GetBytes(this IDataReader reader, string field)
        {
            return (reader[field] as byte[]);
        }
        /// <summary>
        /// 获取指定字段的字符串，如果为空，则返回指定的默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(this IDataReader reader, string field, string defaultValue = null)
        {
            var value = reader[field];
            return (value is string ? (string)value : defaultValue);
        }

        /// <summary>
        /// 获取指定字段的Guid，如果为空，则返回Guid.Empty
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static Guid GetGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as Guid? ?? Guid.Empty;
        }
        /// <summary>
        /// 获取指定字段的可空Guid
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static Guid? GetNullableGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as Guid?;
        }
        /// <summary>
        /// 获取指定字段的DateTime，如果为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDataReader reader, string field)
        {
            return reader.GetDateTime(field, DateTime.MinValue);
        }

        /// <summary>
        /// 获取指定字段的DateTime，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDataReader reader, string field, DateTime defaultValue)
        {
            var value = reader[field];
            return value as DateTime? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的可空DateTime
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTime? GetNullableDateTime(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as DateTime?;
        }
        /// <summary>
        /// 获取指定字段的DateTimeOffset（时间点），如果为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field)
        {
            return new DateTimeOffset(reader.GetDateTime(field), TimeSpan.Zero);
        }
        /// <summary>
        /// 获取指定字段的DateTimeOffset（时间点），如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field, DateTimeOffset defaultValue)
        {
            var dt = reader.GetDateTime(field);
            return (dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue);
        }
        /// <summary>
        /// 获取指定字段的可空DateTimeOffset（时间点）
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataReader reader, string field)
        {
            var dt = reader.GetNullableDateTime(field);
            return (dt != null ? (DateTimeOffset?)new DateTimeOffset(dt.Value, TimeSpan.Zero) : null);
        }
        /// <summary>
        /// 获取指定字段的int值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt32(this IDataReader reader, string field, int defaultValue = 0)
        {
            var value = reader[field];
            return value as int? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的可空int值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static int? GetNullableInt32(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as int?;
        }
        /// <summary>
        /// 获取指定字段的long值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long GetInt64(this IDataReader reader, string field, long defaultValue = 0)
        {
            var value = reader[field];
            return value as long? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的可空long值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static long? GetNullableInt64(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as long?;
        }
        /// <summary>
        /// 获取指定字段的decimal值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetDecimal(this IDataReader reader, string field, decimal defaultValue = 0)
        {
            var value = reader[field];
            return value as decimal? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的可空decimal值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static decimal? GetNullableDecimal(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as decimal?;
        }
        /// <summary>
        /// 获取指定字段的bool值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetBoolean(this IDataReader reader, string field, bool defaultValue = false)
        {
            var value = reader[field];
            return value as bool? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的可空bool值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static bool? GetNullableBoolean(this IDataReader reader, string field)
        {
            var value = reader[field];
            return value as bool?;
        }
        /// <summary>
        /// 获取指定字段的类型实例，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Type GetType(this IDataReader reader, string field, Type defaultValue = null)
        {
            var classType = reader.GetString(field);
            if (!classType.IsNullOrEmpty())
            {
                var type = Type.GetType(classType);
                if (type != null)
                {
                    return type;
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// 获取指定字段的类型实例，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static object GetTypeInstance(this IDataReader reader, string field, Type defaultValue = null)
        {
            var type = reader.GetType(field, defaultValue);
            return (type != null ? Activator.CreateInstance(type) : null);
        }
        /// <summary>
        /// 获取指定字段的泛型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static T GetTypeInstance<T>(this IDataReader reader, string field) where T : class
        {
            return (reader.GetTypeInstance(field, null) as T);
        }
        /// <summary>
        /// 获取指定字段的泛型对象，如果为空，则返回指定类型的实例对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <param name="type">指定实例类型</param>
        /// <returns></returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field, Type type) where T : class
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? Activator.CreateInstance(type) as T);
        }
        /// <summary>
        /// 获取指定字段的泛型对象，如果为空，则返回泛型类型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field) where T : class, new()
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? new T());
        }
        /// <summary>
        /// 确定指定字段的记录值是否为DbNull
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static bool IsDbNull(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value == DBNull.Value);
        }
        /// <summary>
        /// 记录执行次数，从数据读取器中读取所有记录并执行每个操作
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="action">执行操作</param>
        /// <returns></returns>
        public static int ReadAll(this IDataReader reader, Action<IDataReader> action)
        {
            var count = 0;
            while (reader.Read())
            {
                action(reader);
                count++;
            }
            return count;
        }
        /// <summary>
        /// 获取指定名称的列索引（不区分大小写）
        /// </summary>
        /// <param name="this">数据记录</param>
        /// <param name="name">名称</param>
        /// <returns>成功返回列索引，失败则返回-1</returns>
        public static int IndexOf(this IDataRecord @this, string name)
        {
            for (int i = 0; i < @this.FieldCount; i++)
            {
                if (string.Compare(@this.GetName(i), name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
