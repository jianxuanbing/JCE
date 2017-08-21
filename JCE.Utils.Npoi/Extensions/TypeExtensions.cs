using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// 常用类型字典
        /// </summary>
        private static readonly Dictionary<Type, object> CommonTypeDictionary = new Dictionary<Type, object>
        {
            {typeof(Guid), default(Guid)},
            {typeof(TimeSpan), default(TimeSpan)},
            {typeof(DateTime), default(DateTime)},
            {typeof(DateTimeOffset), default(DateTimeOffset)},
            {typeof(char), default(char)},
            {typeof(int), default(int)},
            {typeof(uint), default(uint)},
            {typeof(long), default(long)},
            {typeof(ulong), default(ulong)},
            {typeof(short), default(short)},
            {typeof(ushort), default(ushort)},
            {typeof(byte), default(byte)},
            {typeof(sbyte), default(sbyte)},
            {typeof(bool), default(bool)},
            {typeof(double), default(double)},
            {typeof(float), default(float)},
            {typeof(decimal), default(decimal)},
        };

        /// <summary>
        /// 展开<see cref="Nullable"/>类型，如果<paramref name="type"/>为可空类型或者类型为self，则获取可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static Type UnwrapNullableType(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        /// <summary>
        /// 判断指定类型是否原始类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsPrimitive(this Type type) => type.IsInteger() || type.IsNonIntegerPrimitive();

        /// <summary>
        /// 判断指定类型是否整数类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsInteger(this Type type)
        {
            type = type.UnwrapNullableType();
            return (type == typeof(int))
                   || (type == typeof(long))
                   || (type == typeof(short))
                   || (type == typeof(byte))
                   || (type == typeof(uint))
                   || (type == typeof(ushort))
                   || (type == typeof(ulong))
                   || (type == typeof(sbyte))
                   || (type == typeof(char));
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type type)
        {
            if (!type.GetTypeInfo().IsValueType)
            {
                return null;
            }
            object value;
            return CommonTypeDictionary.TryGetValue(type, out value)
                ? value
                : Activator.CreateInstance(type);
        }

        /// <summary>
        /// 判断指定类型是否非整数的原始类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static bool IsNonIntegerPrimitive(this Type type)
        {
            type = type.UnwrapNullableType();
            return (type == typeof(bool))
                   || (type == typeof(byte[]))
                   || (type == typeof(DateTime))
                   || (type == typeof(TimeSpan))
                   || (type == typeof(DateTimeOffset))
                   || (type == typeof(decimal))
                   || (type == typeof(double))
                   || (type == typeof(float))
                   || (type == typeof(Guid))
                   || (type == typeof(string))
                   || type.GetTypeInfo().IsEnum;
        }

    }
}
