using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Xml扩展 - 转换扩展
    /// </summary>
    public static partial class XmlExtensions
    {
        #region SerializeToXml(对象序列化成Xml字符串)
        /// <summary>
        /// 对象序列化成Xml字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string SerializeToXml<T>(this T obj) where T:class,new()
        {
            string result = string.Empty;
            MemoryStream stream = null;
            StreamReader streamReader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));

                stream = new MemoryStream();
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
            }
            finally
            {
                streamReader?.Dispose();
                stream?.Dispose();
            }
            return result;
        }

        #endregion

        #region DeserializeXml(Xml字符串反序列化成对象)
        /// <summary>
        /// Xml字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">Xml字符串</param>
        /// <returns></returns>
        public static T DeserializeXml<T>(this string xml) where T : class
        {
            return DeserializeXml(xml, typeof(T)) as T;
        }

        /// <summary>
        /// Xml字符串反序列化成对象
        /// </summary>
        /// <param name="xml">Xml字符串</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object DeserializeXml(this string xml, Type type)
        {
            object result = null;
            MemoryStream stream = null;
            StreamWriter writer = null;
            try
            {
                stream=new MemoryStream();

                writer=new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();

                stream.Position = 0;
                var deserialize=new XmlSerializer(type);
                result = deserialize.Deserialize(stream);
            }
            finally
            {
                writer?.Dispose();
                stream?.Dispose();
            }
            return result;
        }
        #endregion
    }
}
