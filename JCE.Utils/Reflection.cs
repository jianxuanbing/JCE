/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Reflection
 * 版本号：v1.0.0.0
 * 唯一标识：94823bc9-7711-4932-ad56-543c5ada7946
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:09:22
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:09:22
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Files;

namespace JCE.Utils
{
    /// <summary>
    /// 反射操作帮助类
    /// </summary>
    public static partial class Reflection
    {
        #region GetAssembly(获取程序集)
        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }
        #endregion

        #region GetDescription(获取描述)
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        /// <returns></returns>
        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(Sys.GetType<T>(), memberName);
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        /// <returns></returns>
        public static string GetDescription(Type type, string memberName)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(memberName))
            {
                return string.Empty;
            }
            return GetDescription(type.GetMember(memberName).FirstOrDefault());
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static string GetDescription(MemberInfo member)
        {
            if (member == null)
            {
                return string.Empty;
            }
            var attribute =
                member.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            return attribute == null ? member.Name : attribute.Description;
        }
        #endregion

        #region CreateInstance(动态创建实例)
        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="className">类名，包括命名空间,如果类型不处于当前执行程序集中，需要包含程序集名，范例：Test.Core.Test2,Test.Core</param>
        /// <param name="parameters">传递给构造函数的参数</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string className, params object[] parameters)
        {
            Type type = Type.GetType(className) ?? Assembly.GetCallingAssembly().GetType(className);
            return CreateInstance<T>(type, parameters);
        }
        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>
        /// <returns></returns>
        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return Conv.To<T>(Activator.CreateInstance(type, parameters));
        }
        #endregion

        #region IsBool(是否布尔类型)
        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }
            return false;
        }
        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns></returns>
        public static bool IsBool(PropertyInfo property)
        {
            if (property.PropertyType == typeof(bool))
            {
                return true;
            }
            if (property.PropertyType == typeof(bool?))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region IsEnum(是否枚举类型)
        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }
        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns></returns>
        public static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.IsEnum)
            {
                return true;
            }
            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
            {
                return false;
            }
            return value.IsEnum;
        }
        #endregion

        #region IsDate(是否日期类型)
        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }
        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns></returns>
        public static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
            {
                return true;
            }
            if (property.PropertyType == typeof(DateTime?))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IsInt(是否整型)
        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int32" || member.ToString() == "System.Int16" ||
                           member.ToString() == "System.Int64";
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }
        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns></returns>
        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
            {
                return true;
            }
            if (property.PropertyType == typeof(int?))
            {
                return true;
            }
            if (property.PropertyType == typeof(short))
            {
                return true;
            }
            if (property.PropertyType == typeof(short?))
            {
                return true;
            }
            if (property.PropertyType == typeof(long))
            {
                return true;
            }
            if (property.PropertyType == typeof(long?))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IsNumber(是否数值类型)
        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        /// <returns></returns>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Double" || member.ToString() == "System.Decimal" ||
                           member.ToString() == "System.Single";
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }
        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns></returns>
        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
            {
                return true;
            }
            if (property.PropertyType == typeof(double?))
            {
                return true;
            }
            if (property.PropertyType == typeof(decimal))
            {
                return true;
            }
            if (property.PropertyType == typeof(decimal?))
            {
                return true;
            }
            if (property.PropertyType == typeof(float))
            {
                return true;
            }
            if (property.PropertyType == typeof(float?))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region GetByInterface(获取实现了接口的所有具体类型)
        /// <summary>
        /// 获取实现了接口的所有具体类型
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="assembly">在该程序集中查找</param>
        /// <returns></returns>
        public static List<T> GetByInterface<T>(Assembly assembly)
        {
            var typeInterface = typeof(T);
            return
                assembly.GetTypes()
                    .Where(t => typeInterface.IsAssignableFrom(t) && t != typeInterface && t.IsAbstract == false)
                    .Select(t => CreateInstance<T>(t))
                    .ToList();
        }
        #endregion

        #region GetAssemblies(从目录中获取所有程序集)
        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = FileUtil.GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFile).ToList();
        }
        #endregion

        #region GetDisplayName(获取显示名称)
        /// <summary>
        /// 获取显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static string GetDisplayName<T>()
        {
            var type = Sys.GetType<T>();
            var attribute = type.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.DisplayName;
        }
        #endregion
    }
}
