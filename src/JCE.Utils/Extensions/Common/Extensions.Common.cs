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

    }
}
