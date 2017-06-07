/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ObjectExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：39c0825f-2044-4790-a326-d57b5b530034
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:34:59
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:34:59
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// object类型的扩展辅助操作类
    /// </summary>
    public static class ObjectExtensions
    {
        #region CastTo(把对象类型转换为指定类型)
        /// <summary>
        /// 把对象类型转换为指定类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="conversionType">指定类型</param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType.IsNullableType())
            {
                conversionType = conversionType.GetUnNullableType();
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 把对象类型转化为指定类型
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败引发异常。 </returns>
        public static T CastTo<T>(this object value)
        {
            object result = CastTo(value, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region IsBetween(判断当前值是否介于指定范围内)
        /// <summary>
        /// 判断当前值是否介于指定范围内
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 动态类型对象 </param>
        /// <param name="start"> 范围起点 </param>
        /// <param name="end"> 范围终点 </param>
        /// <param name="leftEqual"> 是否可等于上限（默认等于） </param>
        /// <param name="rightEqual"> 是否可等于下限（默认等于） </param>
        /// <returns> 是否介于 </returns>
        public static bool IsBetween<T>(this IComparable<T> value, T start, T end, bool leftEqual = false, bool rightEqual = false) where T : IComparable
        {
            bool flag = leftEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
            return flag && (rightEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0);
        }
        #endregion

        #region ToDynamic(将对象转换为dynamic)
        /// <summary>
        /// 将对象[主要是匿名对象]转换为dynamic
        /// </summary>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            Type type = value.GetType();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.ToDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return (ExpandoObject)expando;
        }
        #endregion

        #region Equals(比较)
        /// <summary>
        /// 确定当前对象是否与所提供的值相等
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">要比较的对象</param>
        /// <param name="values">要与对象进行比较的值</param>
        /// <returns></returns>
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return (Array.IndexOf(values, obj) != -1);
        }
        /// <summary>
        /// 确定当前对象是否与所提供的值不相等
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">要比较的对象</param>
        /// <param name="values">要与对象进行比较的值</param>
        /// <returns></returns>
        public static bool EqualsNone<T>(this T obj, params T[] values)
        {
            return (obj.EqualsAny(values) == false);
        }
        #endregion

        #region ConvertTo(转换)
        /// <summary>
        /// 将对象转换为指定的目标类型，并且忽略异常，如果值为空则返回默认值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>目标类型</returns>
        public static T ConvertToAndIgnoreException<T>(this object value, T defaultValue = default(T))
        {
            return value.ConvertTo(defaultValue, true);
        }
        /// <summary>
        /// 将对象转换为指定的目标类型，如果值为空则返回默认值。
        /// 如果该值不能转换，即使类型是可转换为彼此，则抛出异常
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>目标类型</returns>
        public static T ConvertTo<T>(this object value, T defaultValue = default(T))
        {
            if (value != null)
            {
                var targetType = typeof(T);
                if (value.GetType() == targetType)
                {
                    return (T)value;
                }
                var converter = TypeDescriptor.GetConverter(value);
                if (converter.CanConvertTo(targetType))
                {
                    return (T)converter.ConvertTo(value, targetType);
                }
                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(value.GetType()))
                {
                    return (T)converter.ConvertFrom(value);
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// 将对象转换为指定的目标类型，如果值为空则返回默认值。
        /// 如果这两种类型是不可转换的，并且忽略异常则产生异常时返回默认值，否则抛出异常
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="ignoreException">是否忽略异常</param>
        /// <returns>目标类型</returns>
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (ignoreException)
            {
                try
                {
                    return value.ConvertTo<T>();
                }
                catch
                {
                    return defaultValue;
                }
            }
            return value.ConvertTo<T>();
        }
        /// <summary>
        /// 确定该值是否可以转换为指定的目标类型（理论上）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <returns>bool</returns>
        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);
                var converter = TypeDescriptor.GetConverter(value);
                if (converter.CanConvertTo(targetType))
                {
                    return true;
                }
                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertTo(value.GetType()))
                {
                    return true;
                }
            }
            return false;
        }        
        #endregion

        #region InvokeMethod(动态调用方法)
        /// <summary>
        /// 动态调用方法（使用反射）
        /// </summary>
        /// <param name="obj">需要执行的对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">传递的参数</param>
        /// <returns>返回值</returns>
        /// <example>
        /// 	<code>
        /// 		var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// 		var file = type.CreateInstance(@"c:\autoexec.bat");
        /// 		if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        /// 		var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        /// 		Console.WriteLine(reader.ReadToEnd());
        /// 		reader.Close();
        /// 		}
        /// 	</code>
        /// </example>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return obj.InvokeMethod<object>(methodName, parameters);
        }
        /// <summary>
        /// 动态调用方法并将其返回值类型化（使用反射）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">需要执行的对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">传递的参数</param>
        /// <returns>返回值</returns>
        /// <example>
        /// 	<code>
        /// 		var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// 		var file = type.CreateInstance(@"c:\autoexec.bat");
        /// 		if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        /// 		var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        /// 		Console.WriteLine(reader.ReadToEnd());
        /// 		reader.Close();
        /// 		}
        /// 	</code>
        /// </example>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(x => x.GetType()).ToArray());
            if (method == null)
            {
                throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);
            }
            var value = method.Invoke(obj, parameters);
            return (value is T ? (T)value : default(T));
        }
        #endregion

        #region GetPropertyValue(获取属性值)
        /// <summary>
        /// 动态获取属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        /// <example>
        /// 	<code>
        /// 		var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// 		var file = type.CreateInstance(@"c:\autoexec.bat");
        /// 		if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        /// 		var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        /// 		Console.WriteLine(reader.ReadToEnd());
        /// 		reader.Close();
        /// 		}
        /// 	</code>
        /// </example>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetPropertyValue<object>(propertyName, null);
        }
        /// <summary>
        /// 动态获取属性值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        /// <example>
        /// 	<code>
        /// 		var type = Type.GetType("System.IO.FileInfo, mscorlib");
        /// 		var file = type.CreateInstance(@"c:\autoexec.bat");
        /// 		if(file.GetPropertyValue&lt;bool&gt;("Exists")) {
        /// 		var reader = file.InvokeMethod&lt;StreamReader&gt;("OpenText");
        /// 		Console.WriteLine(reader.ReadToEnd());
        /// 		reader.Close();
        /// 		}
        /// 	</code>
        /// </example>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue = default(T))
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }
            var value = property.GetValue(obj, null);
            return (value is T ? (T)value : defaultValue);
        }
        #endregion

        #region SetPropertyValue(设置属性值)
        /// <summary>
        /// 动态设置属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }
            if (!property.CanWrite)
            {
                throw new ArgumentException(string.Format("Property '{0}' does not allow writes.", propertyName),
                    propertyName);
            }
            property.SetValue(obj, value, null);
        }
        #endregion

        #region GetAttribute(获取特性属性)
        /// <summary>
        /// 获取数据类型上定义的特性，第一个
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="includeInherited">是否包含继承的属性，默认包含</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this object obj, bool includeInherited = true) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            return attributes.FirstOrDefault() as T;
        }
        /// <summary>
        /// 获取数据类型上定义的所有匹配特性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="includeInherited">是否包含继承的属性，默认不包含</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited = false)
            where T : Attribute
        {
            return
                (obj as Type ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited)
                    .OfType<T>()
                    .Select(attribute => attribute);
        }
        #endregion

        #region Is(判断)
        /// <summary>
        /// 确定对象是否指定的泛型类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>bool</returns>
        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }
        /// <summary>
        /// 确定对象是否指定的类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>bool</returns>
        public static bool IsOfType(this object obj, Type type)
        {
            return (obj.GetType() == type);
        }
        /// <summary>
        /// 确定对象是否指定的泛型类型或继承类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>bool</returns>
        public static bool IsOfTypeOrInherits<T>(this object obj)
        {
            return obj.IsOfTypeOrInherits(typeof(T));
        }
        /// <summary>
        /// 确定对象是否制定类型或继承类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>bool</returns>
        public static bool IsOfTypeOrInherits(this object obj, Type type)
        {
            var objectType = obj.GetType();
            do
            {
                if (objectType == type)
                {
                    return true;
                }
                if ((objectType == objectType.BaseType) || (objectType.BaseType == null))
                {
                    return false;
                }
                objectType = objectType.BaseType;
            } while (true);
        }
        /// <summary>
        /// 确定对象是否是分配给所传递的泛型类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>bool</returns>
        public static bool IsAssignableTo<T>(this object obj)
        {
            return obj.IsAssignableTo(typeof(T));
        }
        /// <summary>
        /// 确定对象是否是
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>bool</returns>
        public static bool IsAssignableTo(this object obj, Type type)
        {
            var objectType = obj.GetType();
            return type.IsAssignableFrom(objectType);
        }
        /// <summary>
        /// 确定当前对象是否为空
        /// </summary>
        /// <param name="target">当前对象</param>
        /// <returns>bool</returns>
        public static bool IsNull(this object target)
        {
            var ret = target.IsNull<object>();
            return ret;
        }
        /// <summary>
        /// 确定当前泛型对象是否为空
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="target">当前对象</param>
        /// <returns>bool</returns>
        public static bool IsNull<T>(this T target)
        {
            var result = ReferenceEquals(target, null);
            return result;
        }
        #endregion

        /// <summary>
        /// 获取基本数据类型的默认值，引用类型则为空
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <returns>默认值</returns>
        public static T GetTypeDefaultValue<T>(this T value)
        {
            return default(T);
        }
        /// <summary>
        /// 将指定的值转换为数据库中的值，如果指定值等于其默认值，则返回 DBNull.Value
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">值</param>
        /// <returns>对象</returns>
        public static object ToDatabaseValue<T>(this T value)
        {
            return (value.Equals(value.GetTypeDefaultValue()) ? DBNull.Value : (object)value);
        }

        /// <summary>
        /// 使用指定的格式提供程序返回字符串表示形式，如果目标为空，则返回空
        /// </summary>
        /// <param name="target">目标转换为字符串表示形式，可以为空</param>
        /// <param name="formatProvider">格式提供程序，用于将目标转换为字符串表示形式</param>
        /// <returns></returns>
        public static string AsString(this object target, IFormatProvider formatProvider)
        {
            var result = string.Format(formatProvider, "{0}", target);
            return result;
        }
        /// <summary>
        /// 使用默认的格式提供程序返回字符串表示形式，如果目标字符串为空，则返回空
        /// </summary>
        /// <param name="target">目标转换为字符串表示形式，可以为空</param>
        /// <returns></returns>
        public static string AsInvariantString(this object target)
        {
            var result = string.Format(CultureInfo.InvariantCulture, "{0}", target);
            return result;
        }

        #region NotNull(空引用默认值)
        /// <summary>
        /// 如果目标对象是空引用，返回notNullValue
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="notNullValue">为空值时用来代替空值</param>
        /// <returns></returns>
        public static T NotNull<T>(this T target, T notNullValue)
        {
            return ReferenceEquals(target, null) ? notNullValue : target;
        }
        /// <summary>
        /// 如果目标对象是空引用，返回notNullValue
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="notNullValueProvider">空值时用来代替空值</param>
        /// <returns></returns>
        public static T NotNull<T>(this T target, Func<T> notNullValueProvider)
        {
            return ReferenceEquals(target, null) ? notNullValueProvider() : target;
        }
        #endregion

        #region ToStringDump(获取对象字符串表示形式)
        /// <summary>
        /// 获取指定对象的字符串表示形式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="flags">标识，用于反射</param>
        /// <param name="maxArrayElements">最大数组长度</param>
        /// <returns></returns>
        public static string ToStringDump(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            int maxArrayElements = 5)
        {
            return
                ToStringDumpInternal(obj.ToXElement(flags, maxArrayElements))
                    .Aggregate(new StringBuilder(), (sb, el) => sb.Append(el))
                    .ToString();
        }
        /// <summary>
        /// 获取指定对象的字符串表示形式的实现方法
        /// </summary>
        /// <param name="toXElemnt">节点</param>
        /// <returns></returns>
        static IEnumerable<string> ToStringDumpInternal(XContainer toXElemnt)
        {
            foreach (var xElement in toXElemnt.Elements().OrderBy(x => x.Name.ToString()))
            {
                if (xElement.HasElements)
                {
                    foreach (var el in ToStringDumpInternal(xElement))
                    {
                        yield return "{" + string.Format("{0}={1}", xElement.Name, el) + "}";
                    }
                }
                else
                {
                    yield return "{" + string.Format("{0}={1}", xElement.Name, xElement.Value) + "}";
                }
            }
        }
        #endregion

        #region ToHtmlTable(获取对象Html表格的表示形式)
        /// <summary>
        /// 获取指定对象Html表格的表示形式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="flags">标识，用于反射</param>
        /// <param name="maxArrayElements">最大数组长度</param>
        /// <returns></returns>
        public static string ToHtmlTable(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            int maxArrayElements = 5)
        {
            return ToHtmlTableInternal(obj.ToXElement(flags, maxArrayElements), 0)
                .Aggregate(string.Empty, (str, el) => str + el);
        }
        /// <summary>
        /// 获取指定对象Html表格的表示形式的实现方法
        /// </summary>
        /// <param name="xel">节点</param>
        /// <param name="padding">填充数</param>
        /// <returns></returns>
        static IEnumerable<string> ToHtmlTableInternal(XContainer xel, int padding)
        {
            yield return FormatHtmlLine("<table>", padding);
            yield return FormatHtmlLine("<tr><th>Attribute</th><th>Value</th></tr>", padding + 1);
            foreach (var xElement in xel.Elements().OrderBy(x => x.Name.ToString()))
            {
                if (xElement.HasElements)
                {
                    yield return FormatHtmlLine(string.Format("<tr><td>{0}</td><td>", xElement.Name), padding + 1);
                    foreach (var el in ToHtmlTableInternal(xElement, padding + 2))
                    {
                        yield return el;
                    }
                    yield return FormatHtmlLine("</td></tr>", padding + 1);
                }
                else
                {
                    yield return
                        FormatHtmlLine(
                            string.Format("<tr><td>{0}</td><td>{1}</td></tr>", xElement.Name,
                                HttpUtility.HtmlEncode(xElement.Value)), padding + 1);
                }
            }
            yield return FormatHtmlLine("</table>", padding);
        }
        /// <summary>
        /// 格式化Html行
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="padding">填充数量</param>
        /// <returns></returns>
        static string FormatHtmlLine(string tag, int padding)
        {
            return string.Format("{0}{1}{2}", string.Empty.PadRight(padding, '\t'), tag, Environment.NewLine);
        }
        #endregion

        #region ToXElement(获取指定对象的Xml表示形式)
        /// <summary>
        /// 获取指定对象的Xml表示形式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="flags">标识，用于反射</param>
        /// <param name="maxArrayElements">最大数组长度</param>
        /// <returns></returns>
        public static XElement ToXElement(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            int maxArrayElements = 5)
        {
            try
            {
                return ToXElementInternal(obj, new HashSet<object>(), flags, maxArrayElements);
            }
            catch
            {
                return new XElement(obj.GetType().Name);
            }
        }
        /// <summary>
        /// 获取指定对象的Xml表示形式的实现方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="visited">集合</param>
        /// <param name="flags">标识，用于反射</param>
        /// <param name="maxArrayElements">最大数组长度</param>
        /// <returns></returns>
        static XElement ToXElementInternal(object obj, ICollection<object> visited, BindingFlags flags,
            int maxArrayElements)
        {
            if (obj == null)
            {
                return new XElement("null");
            }
            if (visited.Contains(obj))
            {
                return new XElement("cyclicreference");
            }
            if (!obj.GetType().IsValueType)
            {
                visited.Add(obj);
            }
            var type = obj.GetType();
            var elems = new XElement(CleanName(type.Name, type.IsArray));
            if (!NeedRecursion(type, obj))
            {
                elems.Add(new XElement(CleanName(type.Name, type.IsArray), string.Empty + obj));
                return elems;
            }
            if (obj is IEnumerable)
            {
                var i = 0;
                foreach (var el in obj as IEnumerable)
                {
                    var subType = el.GetType();
                    elems.Add(NeedRecursion(subType, el)
                        ? ToXElementInternal(el, visited, flags, maxArrayElements)
                        : new XElement(CleanName(subType.Name, subType.IsArray), el));
                    if (i++ >= maxArrayElements)
                    {
                        break;
                    }
                }
                return elems;
            }
            foreach (
                var propertyInfo in
                    from propertyInfo in type.GetProperties(flags) where propertyInfo.CanRead select propertyInfo)
            {
                var value = GetValue(obj, propertyInfo);
                elems.Add(NeedRecursion(propertyInfo.PropertyType, value)
                    ? new XElement(CleanName(propertyInfo.Name, propertyInfo.PropertyType.IsArray),
                        ToXElementInternal(value, visited, flags, maxArrayElements))
                    : new XElement(CleanName(propertyInfo.Name, propertyInfo.PropertyType.IsArray), string.Empty + value));
            }
            foreach (var fieldInfo in type.GetFields())
            {
                var value = fieldInfo.GetValue(obj);
                elems.Add(NeedRecursion(fieldInfo.FieldType, value)
                    ? new XElement(CleanName(fieldInfo.Name, fieldInfo.FieldType.IsArray),
                        ToXElementInternal(value, visited, flags, maxArrayElements))
                    : new XElement(CleanName(fieldInfo.Name, fieldInfo.FieldType.IsArray), string.Empty + value));
            }
            return elems;
        }
        /// <summary>
        /// 需要递归
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        static bool NeedRecursion(Type type, object obj)
        {
            return obj != null &&
                   (!type.IsPrimitive &&
                    !(obj is String || obj is DateTime || obj is DateTimeOffset || obj is TimeSpan || obj is Delegate ||
                      obj is Enum || obj is Decimal || obj is Guid));
        }
        /// <summary>
        /// 洁净的名字
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="isArray">是否数组</param>
        /// <returns></returns>
        static string CleanName(IEnumerable<char> name, bool isArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in name.Where(x => char.IsLetterOrDigit(x) && x != '`').Select(x => x))
            {
                sb.Append(c);
            }
            if (isArray)
            {
                sb.Append("Array");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyInfo">属性信息</param>
        /// <returns></returns>
        static object GetValue(object obj, PropertyInfo propertyInfo)
        {
            object value;
            try
            {
                value = propertyInfo.GetValue(obj, null);
            }
            catch
            {
                try
                {
                    value = propertyInfo.GetValue(obj, new object[] { 0 });
                }
                catch
                {
                    value = null;
                }
            }
            return value;
        }
        #endregion

        #region Cast(强制转换对象为指定类型)
        /// <summary>
        /// 强制转换对象为指定类型，对匿名类型有用（动态类转换）
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public static object DynamicCast(this object obj, Type targetType)
        {
            //如果类型相同，则直接返回对象
            if (targetType.IsInstanceOfType(obj))
            {
                return obj;
            }
            //如果不相同，则需要找到强制转换运算符
            const BindingFlags pubStatBinding = BindingFlags.Public | BindingFlags.Static;
            var originType = obj.GetType();
            string[] names = { "op_Implicit", "op_Explicit" };
            var castMethod =
                targetType.GetMethods(pubStatBinding)
                    .Union(originType.GetMethods(pubStatBinding))
                    .FirstOrDefault(
                        itm =>
                            itm.ReturnType == targetType && itm.GetParameters().Length == 1 &&
                            itm.GetParameters()[0].ParameterType.IsAssignableFrom(originType) &&
                            names.Contains(itm.Name));
            if (null != castMethod)
            {
                return castMethod.Invoke(null, new[] { obj });
            }
            throw new InvalidOperationException(string.Format("No matching cast operator found from {0} to {1}.",
                originType.Name, targetType.Name));
        }
        /// <summary>
        /// 强制转换对象为指定类型（实体类转换）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">当前对象</param>
        /// <returns></returns>
        public static T CastAs<T>(this object obj) where T : class, new()
        {
            return obj as T;
        }
        /// <summary>
        /// 强制转换基本数据类型为指定类型，如果为空则返回默认值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T Cast<T>(this object obj, T defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }
        #endregion

        #region CountLoopsToNull(循环计数Null总数)
        /// <summary>
        /// 循环计数Null总数
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="item">需要执行的项</param>
        /// <param name="function">需要执行的函数</param>
        /// <returns></returns>
        public static int CountLoopsToNull<T>(this T item, Func<T, T> function) where T : class
        {
            var num = 0;
            while ((item = function(item)) != null)
            {
                num++;
            }
            return num;
        }
        #endregion

        #region FindTypeByRecursion(递归查找类型实例)
        /// <summary>
        /// 递归查找类型实例
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <typeparam name="K">目标类型</typeparam>
        /// <param name="item">需要执行的项</param>
        /// <param name="function">需要执行的函数</param>
        /// <returns>目标类型或Null</returns>
        public static K FindTypeByRecursion<T, K>(this T item, Func<T, T> function)
            where T : class
            where K : class, T
        {
            do
            {
                if (item is K)
                {
                    return (K)item;
                }
            } while ((item = function(item)) != null);
            return null;
        }
        #endregion

        #region Copy(复制)
        /// <summary>
        /// 对象深拷贝
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// 复制属性值，将源对象中可读和可写的公共属性值复制到目标对象
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="source">源对象</param>
        public static void CopyPropertiesFrom(this object target, object source)
        {
            CopyPropertiesFrom(target, source, string.Empty);
        }
        /// <summary>
        /// 复制属性值，将源对象中可读和可写的公共属性值复制到目标对象
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="source">源对象</param>
        /// <param name="ignoreProperty">忽略的属性</param>
        public static void CopyPropertiesFrom(this object target, object source, string ignoreProperty)
        {
            CopyPropertiesFrom(target, source, new[] { ignoreProperty });
        }
        /// <summary>
        /// 复制属性值，将源对象中可读和可写的公共属性值复制到目标对象
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="source">源对象</param>
        /// <param name="ignoreProperties">忽略的属性集合</param>
        public static void CopyPropertiesFrom(this object target, object source, string[] ignoreProperties)
        {
            //获取并检查对象类型
            Type type = source.GetType();
            if (target.GetType() != type)
            {
                throw new ArgumentException("The source type must be the same as the target");
            }
            //构建需要忽略复制的属性名称的列表
            var ignoreList = new List<string>();
            foreach (string item in ignoreProperties)
            {
                if (!string.IsNullOrEmpty(item) && !ignoreList.Contains(item))
                {
                    ignoreList.Add(item);
                }
            }
            //复制属性
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.CanWrite && property.CanRead && !ignoreList.Contains(property.Name))
                {
                    object value = property.GetValue(source, null);
                    property.SetValue(target, value, null);
                }
            }
        }
        #endregion

        #region ToPropertiesString(获取对象属性值的字符串表示形式)
        /// <summary>
        /// 获取对象属性值的字符串表示形式
        /// </summary>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static string ToPropertiesString(this object source)
        {
            return ToPropertiesString(source, Environment.NewLine);
        }
        /// <summary>
        /// 获取对象属性值的字符串表示形式
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns></returns>
        public static string ToPropertiesString(this object source, string delimiter)
        {
            if (source == null)
            {
                return string.Empty;
            }
            Type type = source.GetType();
            StringBuilder sb = new StringBuilder(type.Name);
            sb.Append(delimiter);
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object val = property.GetValue(source, null);
                    sb.Append(property.Name);
                    sb.Append(": ");
                    sb.Append(val == null ? "[NULL]" : val.ToString());
                    sb.Append(delimiter);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region ToXml(序列化对象为Xml字符串)
        /// <summary>
        /// 将对象序列化为Xml字符串
        /// </summary>
        /// <param name="source">对象</param>
        /// <returns></returns>
        public static string ToXml(this object source)
        {
            return ToXml(source, Const.DefaultEncoding);
        }
        /// <summary>
        /// 将对象序列化为Xml字符串
        /// </summary>
        /// <param name="source">对象</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string ToXml(this object source, Encoding encoding)
        {
            if (source == null)
            {
                throw new ArgumentException("The source object cannot be null.");
            }

            if (encoding == null)
            {
                throw new Exception("You must specify an encoder to use for serialization.");
            }

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(source.GetType());
                serializer.Serialize(stream, source);
                stream.Position = 0;
                return encoding.GetString(stream.ToArray());
            }
        }
        /// <summary>
        /// 将对象序列化为Xml字符串
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="this">对象</param>
        /// <returns></returns>
        public static string ToXml<T>(this T @this)
        {
            if (@this == null)
            {
                throw new NullReferenceException();
            }

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                ser.Serialize(writer, @this);
                return writer.ToString();
            }
        }
        #endregion

        #region ExceptionIfNullOrEmpty(空对象异常)
        /// <summary>
        /// 如果对象为空，则抛出指定消息异常
        /// </summary>
        /// <param name="value">测试对象</param>
        /// <param name="message">消息</param>
        /// <param name="name">参数名</param>
        public static void ExceptionIfNullOrEmpty(this object value, string message, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name, message);
            }
        }
        #endregion

        #region CopyObj(对象拷贝)
        /// <summary>
        /// 对象拷贝，深拷贝
        /// </summary>
        /// <param name="obj">被复制的对象</param>
        /// <returns>新对象</returns>
        public static object CopyObj(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            
            if (targetType.IsValueType == true)
            {
                //值类型
                targetDeepCopyObj = obj;
            }
            else
            {          
                //引用类型
                targetDeepCopyObj = Activator.CreateInstance(targetType);//创建引用对象
                MemberInfo[] memberCollection = obj.GetType().GetMembers();
                foreach (MemberInfo memberInfo in memberCollection)
                {                    
                    if (memberInfo.MemberType == MemberTypes.Field)
                    {
                        //拷贝字段
                        FieldInfo field = (FieldInfo) memberInfo;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj,(fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj,CopyObj(fieldValue));
                        }
                    }
                    else if (memberInfo.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo property = (PropertyInfo) memberInfo;
                        MemberInfo info = property.GetSetMethod(false);
                        if (info != null)
                        {
                            try
                            {
                                object propertyValue = property.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    property.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone());
                                }
                                else
                                {
                                    property.SetValue(targetDeepCopyObj,CopyObj(propertyValue),null);
                                }
                            }
                            catch (Exception ex)
                            {                                                                
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }
        #endregion
    }
}
