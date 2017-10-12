/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：JsonExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：fcd19f59-404f-4aab-afa5-99c291dc9256
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:51:03
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:51:03
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using Json.Net;
using Json.Net.Serialization;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Json字符串扩展
    /// </summary>
    public static class JsonExtensions
    {
        #region 序列化Json(Json.Net)
        #region dynamic(动态对象)
        /// <summary>
        /// Json字符串转为对象，Json.Net
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns>对象</returns>
        public static object JsonNetDeserializeToObj(this string json)
        {
            json.CheckNotNull("json");
            try
            {
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 将对象序列化为JSON字符串，不支持存在循环引用的对象，Json.Net
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>Json字符串</returns>
        public static string JsonNetSerializeToJson(this object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region fixed(固定对象)
        /// <summary>
        /// Json字符串转为对象，Json.Net
        /// </summary>
        /// <typeparam name="T">要转换的目标类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>对象</returns>
        public static T JsonNetDeserializeToObj<T>(this string json)
        {
            json.CheckNotNull("json");
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 将对象序列化为JSON字符串，不支持存在循环引用的对象，Json.Net
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">对象</param>
        /// <returns>Json字符串</returns>
        public static string JsonNetSerializeToJson<T>(this T value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        #region To(Json.Net扩展转换-参考老何)
        /// <summary>
        /// 将Json字符串转换为动态对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static dynamic ToObject(this string json)
        {
            if (json.IsEmpty())
            {
                return "";
            }
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            if (json.IsEmpty())
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
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
            if (target == null)
            {
                return "{}";
            }
            var options = new JsonSerializerSettings();
            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }
            var result = JsonConvert.SerializeObject(target, options);
            if (isConvertToSingleQuotes)
            {
                result = result.Replace("\"", "'");
            }
            return result;
        }
        /// <summary>
        /// 将对象转换为Json字符串，并且去除两侧括号
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertSingleQuotes">是否将双引号转成单引号</param>
        /// <returns></returns>
        public static string ToJsonWithoutBrackets(this object target, bool isConvertSingleQuotes = false)
        {
            var result = ToJson(target, isConvertSingleQuotes);
            if (result == "{}")
            {
                return result;
            }
            return result.TrimStart('{').TrimEnd('}');
        }
        #endregion
        #endregion
        //#region 序列化Json(JavaScirpt)
        //#region dynamic(动态对象)
        ///// <summary>
        ///// 将对象序列化为Json字符串，JavaScripte
        ///// </summary>        
        ///// <param name="value">动态类型对象</param>
        ///// <returns>Json字符串</returns>
        //public static string JavaScriptSerializeToJson(this object value)
        //{
        //    try
        //    {
        //        JavaScriptSerializer javaScripteSerializer = new JavaScriptSerializer();
        //        return javaScripteSerializer.Serialize(value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Json字符串转为对象，JavaScripte
        ///// </summary>        
        ///// <param name="json">Json字符串</param>
        ///// <returns>对象</returns>
        //public static object JavaScriptDeserializeToObj(this string json)
        //{
        //    try
        //    {
        //        JavaScriptSerializer javaScripteSerializer = new JavaScriptSerializer();
        //        return javaScripteSerializer.DeserializeObject(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //#endregion
        //#region fixed(固定对象)
        ///// <summary>
        ///// 将对象序列化为Json字符串，JavaScripte
        ///// </summary>
        ///// <typeparam name="T">目标类型</typeparam>
        ///// <param name="value">动态类型对象</param>
        ///// <returns>Json字符串</returns>
        //public static string JavaScriptSerializeToJson<T>(this T value)
        //{
        //    try
        //    {
        //        JavaScriptSerializer javaScripteSerializer = new JavaScriptSerializer();
        //        return javaScripteSerializer.Serialize(value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Json字符串转为对象，JavaScripte
        ///// </summary>
        ///// <typeparam name="T">要转换的目标类型</typeparam>
        ///// <param name="json">Json字符串</param>
        ///// <returns>对象</returns>
        //public static T JavaScriptDeserializeToObj<T>(this string json)
        //{
        //    try
        //    {
        //        var javaScripteSerializer = new JavaScriptSerializer();
        //        return javaScripteSerializer.Deserialize<T>(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //#endregion
        //#endregion
        //#region 序列化Json(DataContractJsonSerializer)
        ///// <summary>
        ///// 对象序列化为Json字符串，DataContractJsonSerializer
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="value">对象</param>
        ///// <returns>Json字符串</returns>
        //public static string JsonSerializerObjToJson<T>(this T value)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
        //        jsonSerializer.WriteObject(stream, value);
        //        var str = Encoding.UTF8.GetString(stream.ToArray());
        //        return str;
        //    }
        //}
        ///// <summary>
        ///// Json字符串反序列为对象，DataContractJsonSerializer
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="json">json字符串</param>
        ///// <returns>对象</returns>
        //public static T JsonSerializerJsonToObj<T>(this string json)
        //{
        //    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        //    {
        //        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
        //        return (T)jsonSerializer.ReadObject(stream);
        //    }
        //}
        //#endregion
    }
}
