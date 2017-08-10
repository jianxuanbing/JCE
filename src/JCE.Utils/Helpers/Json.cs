using Newtonsoft.Json;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// Json操作
    /// </summary>
    public static class Json
    {
        #region ToObject(将Json字符串转换为对象)
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
        #endregion

        #region ToJson(将对象转换为Json字符串)
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转换成单引号</param>
        /// <returns></returns>
        public static string ToJson(object target, bool isConvertToSingleQuotes = false)
        {
            if (target == null)
            {
                return "{}";
            }
            var result = JsonConvert.SerializeObject(target);
            if (isConvertToSingleQuotes)
            {
                result = result.Replace("\"", "'");
            }
            return result;
        }
        #endregion

    }
}
