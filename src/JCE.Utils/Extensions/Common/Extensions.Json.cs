namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - Json扩展
    /// </summary>
    public static partial class Extensions
    {
        #region ToObject(将Json字符串转换为对象)
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JCE.Utils.Helpers.Json.ToObject<T>(json);
        }
        #endregion

        #region ToJson(将对象转换为Json字符串)

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转换成单引号</param>
        /// <param name="camelCase">是否驼峰式命名</param>
        /// <param name="indented">是否缩进</param>
        /// <returns></returns>
        public static string ToJson(this object target, bool isConvertToSingleQuotes = false, bool camelCase = false, bool indented = false)
        {
            return JCE.Utils.Helpers.Json.ToJson(target, isConvertToSingleQuotes, camelCase, indented);
        }
        #endregion
    }
}
