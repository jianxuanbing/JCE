using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// 转换 扩展
    /// </summary>
    internal static class ConvertExtensions
    {
        /// <summary>
        /// 将对象实例转换成指定类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="conversionType">转换类型</param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType.IsNullableType())
            {
                conversionType = Nullable.GetUnderlyingType(conversionType);
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString(), true);
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            if (conversionType == typeof(bool))
            {
                switch (value.ToString().ToLower())
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    case "是":
                        return true;
                    case "否":
                        return false;
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    case "true":
                        return true;
                    case "false":
                        return false;
                }
            }
            if (value is IConvertible)
            {
                return Convert.ChangeType(value, conversionType);
            }
            return value;
        }

        /// <summary>
        /// 将对象实例转换成指定类型
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static T CastTo<T>(this object value)
        {
            object result = value.CastTo(typeof(T));
            return (T) result;
        }

        /// <summary>
        /// 将对象实例转换成指定类型
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                T res = value.CastTo<T>();
                Type type = typeof(T);
                if (type.IsNullableType() && res == null)
                {
                    return defaultValue;
                }
                if (!type.IsNullableType() && res == null)
                {
                    return defaultValue;
                }
                return res;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 是否可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            // 是否为泛型，是否为Nullable<>的定义类型
            bool result = type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            return result;
        }
    }
}
