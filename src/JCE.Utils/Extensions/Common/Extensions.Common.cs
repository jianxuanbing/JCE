using System;
using System.ComponentModel;

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

        #region Value(获取枚举值)
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static int Value(this System.Enum instance)
        {
            return JCE.Utils.Helpers.Enum.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static TResult Value<TResult>(this System.Enum instance)
        {
            return JCE.Utils.Helpers.Convert.To<TResult>(Value(instance));
        }
        #endregion

        #region Description(获取枚举描述)
        /// <summary>
        /// 获取枚举描述，使用<see cref="DescriptionAttribute"/>特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static string Description(this System.Enum instance)
        {
            return JCE.Utils.Helpers.Enum.GetDescription(instance.GetType(), instance);
        }
        #endregion
    }
}
