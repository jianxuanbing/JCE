using System;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - 公共扩展
    /// </summary>
    public static partial class Extensions
    {
        #region SafeValue(安全获取值)
        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">可空值</param>
        /// <returns></returns>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        #endregion

        #region IsEmpty(是否为空)
        /// <summary>
        /// 字符串是否为空、null或空白字符串
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Guid是否为空、null或Guid.Empty
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == null)
            {
                return true;
            }
            return IsEmpty(value.Value);
        }

        /// <summary>
        /// Guid是否为空、null或Guid.Empty
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid value)
        {
            if (value == Guid.Empty)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IsNull(是否为空)
        /// <summary>
        /// 目标对象是否为空
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public static bool IsNull(this object target)
        {
            return target.IsNull<object>();
        }

        /// <summary>
        /// 目标对象是否为空
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public static bool IsNull<T>(this T target)
        {
            var result = ReferenceEquals(target, null);
            return result;
        }
        #endregion
    }
}
