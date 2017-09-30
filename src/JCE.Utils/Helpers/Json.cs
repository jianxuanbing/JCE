using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// Json 操作
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
        /// <param name="camelCase">是否驼峰式命名</param>
        /// <param name="indented">是否缩进</param>
        /// <returns></returns>
        public static string ToJson(object target, bool isConvertToSingleQuotes = false, bool camelCase = false, bool indented = false)
        {
            if (target == null)
            {
                return "{}";
            }
            var options=new JsonSerializerSettings();
            if (camelCase)
            {
                options.ContractResolver=new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }
            var result = JsonConvert.SerializeObject(target,options);
            if (isConvertToSingleQuotes)
            {
                result = result.Replace("\"", "'");
            }
            return result;
        }
        #endregion

    }
}
