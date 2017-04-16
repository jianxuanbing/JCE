/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DataRowExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b54ff4ab-67ad-427b-80ba-33873142212a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:42:10
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:42:10
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 数据行（DataRow）扩展
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// 获取指定字段的数据类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Get<T>(this DataRow row, string field, T defaultValue = default(T))
        {
            var value = row[field];
            if (value == DBNull.Value)
            {
                return defaultValue;
            }
            return value.ConvertTo(defaultValue);
        }

        /// <summary>
        /// 获取指定字段的字节数组
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static byte[] GetBytes(this DataRow row, string field)
        {
            return (row[field] as byte[]);
        }

        /// <summary>
        /// 获取指定字段的字符串，如果为空，则返回指定的默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(this DataRow row, string field, string defaultValue = null)
        {
            var value = row[field];
            return (value is string ? (string)value : defaultValue);
        }

        /// <summary>
        /// 获取指定字段的Guid，如果为空，则返回Guid.Empty
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static Guid GetGuid(this DataRow row, string field)
        {
            var value = row[field];
            return value as Guid? ?? Guid.Empty;
        }

        /// <summary>
        /// 获取指定字段的DateTime，如果为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this DataRow row, string field)
        {
            return row.GetDateTime(field, DateTime.MinValue);
        }

        /// <summary>
        /// 获取指定字段的DateTime，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this DataRow row, string field, DateTime defaultValue)
        {
            var value = row[field];
            return value as DateTime? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的DateTimeOffset（时间点），如果为空，则返回DateTime.MinValue
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field)
        {
            return new DateTimeOffset(row.GetDateTime(field), TimeSpan.Zero);
        }
        /// <summary>
        /// 获取指定字段的DateTimeOffset（时间点），如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field, DateTimeOffset defaultValue)
        {
            var dt = row.GetDateTime(field);
            return (dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue);
        }
        /// <summary>
        /// 获取指定字段的int值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt32(this DataRow row, string field, int defaultValue = 0)
        {
            var value = row[field];
            return value as int? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的long值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long GetInt64(this DataRow row, string field, long defaultValue = 0)
        {
            var value = row[field];
            return value as long? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的decimal值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetDecimal(this DataRow row, string field, decimal defaultValue = 0)
        {
            var value = row[field];
            return value as decimal? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的bool值，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetBoolean(this DataRow row, string field, bool defaultValue = false)
        {
            var value = row[field];
            return value as bool? ?? defaultValue;
        }
        /// <summary>
        /// 获取指定字段的类型实例，如果为空，则返回指定默认值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Type GetType(this DataRow row, string field, Type defaultValue = null)
        {
            var classType = row.GetString(field);
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
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static object GetTypeInstance(this DataRow row, string field, Type defaultValue = null)
        {
            var type = row.GetType(field, defaultValue);
            return (type != null ? Activator.CreateInstance(type) : null);
        }
        /// <summary>
        /// 获取指定字段的泛型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static T GetTypeInstance<T>(this DataRow row, string field) where T : class
        {
            return (row.GetTypeInstance(field, null) as T);
        }
        /// <summary>
        /// 获取指定字段的泛型对象，如果为空，则返回指定类型的实例对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <param name="type">指定实例类型</param>
        /// <returns></returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field, Type type) where T : class
        {
            var instance = (row.GetTypeInstance(field, null) as T);
            return (instance ?? Activator.CreateInstance(type) as T);
        }
        /// <summary>
        /// 获取指定字段的泛型对象，如果为空，则返回泛型类型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field) where T : class, new()
        {
            var instance = (row.GetTypeInstance(field, null) as T);
            return (instance ?? new T());
        }
        /// <summary>
        /// 确定指定字段的记录值是否为DbNull
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static bool IsDbNull(this DataRow row, string field)
        {
            var value = row[field];
            return (value == DBNull.Value);
        }

        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        public static Func<DataRow, T> ToExpression<T>(this DataRow row) where T : class, new()
        {
            if (row == null)
            {
                throw new ArgumentNullException("row", "当前对象为null无法转换为实体");
            }
            ParameterExpression parameter = Expression.Parameter(typeof(DataRow), "dr");
            List<MemberBinding> binds = new List<MemberBinding>();
            for (int i = 0; i < row.ItemArray.Length; i++)
            {
                string colName = row.Table.Columns[i].ColumnName;
                PropertyInfo info = typeof(T).GetProperty(colName);
                if (info == null)
                {
                    continue;
                }
                MethodInfo methodInfo =
                    typeof(DataRowExtensions).GetMethod("Field", new Type[] { typeof(DataRow), typeof(string) })
                        .MakeGenericMethod(info.PropertyType);
                MethodCallExpression call = Expression.Call(methodInfo, parameter,
                    Expression.Constant(colName, typeof(string)));
                MemberAssignment bind = Expression.Bind(info, call);
                binds.Add(bind);
            }
            MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(T)), binds.ToArray());
            return Expression.Lambda<Func<DataRow, T>>(init, parameter).Compile();
        }
    }
}
