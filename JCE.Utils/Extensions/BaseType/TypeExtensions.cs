/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：TypeExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：de4caa34-cb0d-4168-805a-b200582f4721
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:36:03
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:36:03
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Type类型扩展
    /// </summary>
    public static class TypeExtensions
    {
        #region IsNullableType(判断类型是否为Nullable类型)
        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"> 要处理的类型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {
            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        #endregion

        #region GetNonNummableType(由类型的Nullable类型返回实际类型)
        /// <summary>
        /// 由类型的Nullable类型返回实际类型
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetNonNummableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }
        #endregion

        #region GetUnNullableType(获取Nullable类型的基础类型)
        /// <summary>
        /// 获取Nullable类型的基础类型，通过类型转换器
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                NullableConverter nullableConverter = new NullableConverter(type);
                return nullableConverter.UnderlyingType;
            }
            return type;
        }
        #endregion

        #region ToDescription(获取类型的Description特性描述信息)
        /// <summary>
        /// 获取类型的Description特性描述信息
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="inherit">是否搜索类型的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
        public static string ToDescription(this Type type, bool inherit = false)
        {
            DescriptionAttribute desc = type.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? type.FullName : desc.Description;
        }

        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string ToDescription(this MemberInfo member, bool inherit = false)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            if (desc != null)
            {
                return desc.Description;
            }
            DisplayNameAttribute displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
            if (displayName != null)
            {
                return displayName.DisplayName;
            }
            DisplayAttribute display = member.GetAttribute<DisplayAttribute>(inherit);
            if (display != null)
            {
                return display.Name;
            }
            return member.Name;
        }
        #endregion

        #region HasAttribute(检查指定类型成员中是否存在指定的Attribute特性)
        /// <summary>
        /// 检查指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
            //return memberInfo.GetCustomAttributes(typeof(T), inherit).Any(m => (m as T) != null);
        }
        #endregion

        #region GetAttribute(从类型成员获取指定Attribute特性)
        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            var descripts = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return descripts.FirstOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>返回所有指定Attribute特性的数组</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }
        #endregion

        #region IsEnumerable(判断类型是否为集合类型)
        /// <summary>
        /// 判断类型是否为集合类型
        /// </summary>
        /// <param name="type">要处理的类型</param>
        /// <returns>是返回True，不是返回False</returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
        #endregion

        #region IsGenericAssignableFrom(判断当前泛型类型是否可由指定类型的实例填充)
        /// <summary>
        /// 判断当前泛型类型是否可由指定类型的实例填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericType, Type type)
        {
            genericType.CheckNotNull("genericType");
            type.CheckNotNull("type");
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");
            }

            List<Type> allOthers = new List<Type> { type };
            if (genericType.IsInterface)
            {
                allOthers.AddRange(type.GetInterfaces());
            }

            foreach (var other in allOthers)
            {
                Type cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                    {
                        cur = cur.GetGenericTypeDefinition();
                    }
                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                    {
                        return true;
                    }
                    cur = cur.BaseType;
                }
            }
            return false;
        }
        #endregion

        #region IsAsync(方法是否是异步)
        /// <summary>
        /// 方法是否是异步
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }
        #endregion

        #region IsBaseOn(当前类型是否是指定基类的派生类)
        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="baseType">要判断的基类型</param>
        /// <returns></returns>
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (type.IsGenericTypeDefinition)
            {
                return baseType.IsGenericAssignableFrom(type);
            }
            return baseType.IsAssignableFrom(type);
        }
        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <typeparam name="TBaseType">要判断的基类型</typeparam>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsBaseOn<TBaseType>(this Type type)
        {
            Type baseType = typeof(TBaseType);
            return type.IsBaseOn(baseType);
        }
        #endregion

        #region CreateInstance(创建实例)
        /// <summary>
        /// 创建实例，返回所需类型实例
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="constructorParameters">可选的构造函数参数</param>
        /// <returns>实例</returns>
        public static object CreateInstance(this Type type, params object[] constructorParameters)
        {
            return CreateInstance<object>(type, constructorParameters);
        }
        /// <summary>
        /// 创建实例，返回泛型参数类型T所需类型的实例
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="type">Type</param>
        /// <param name="constructorParameters">可选的构造函数参数</param>
        /// <returns>泛型实例</returns>
        public static T CreateInstance<T>(this Type type, params object[] constructorParameters)
        {
            var instance = Activator.CreateInstance(type, constructorParameters);
            return (T)instance;
        }
        #endregion

        #region IsBaseType(判断当前类型是是否基类型)
        /// <summary>
        /// 判断当前类型是是否基类型
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="checkingType">检查类型</param>
        /// <returns>bool</returns>
        public static bool IsBaseType(this Type type, Type checkingType)
        {
            while (type != typeof(object))
            {
                if (type == null)
                {
                    continue;
                }
                if (type == checkingType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
        #endregion

        #region IsSubclassOfRawGeneric(判断当前类型是否子类的泛型类型)
        /// <summary>
        /// 判断当前类型是否子类的泛型类型
        /// </summary>
        /// <param name="generic">泛型</param>
        /// <param name="toCheck">检查类型</param>
        /// <returns>bool</returns>
        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != typeof(object))
            {
                if (toCheck == null)
                {
                    continue;
                }
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        #endregion

        #region CreateGenericTypeInstance(创建泛型类型实例)
        /// <summary>
        /// 创建泛型类型实例
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="genericType">Type</param>
        /// <param name="typeArguments">类型实参数组</param>
        /// <returns>泛型实例</returns>
        public static T CreateGenericTypeInstance<T>(this Type genericType, params Type[] typeArguments) where T : class
        {
            var constructedType = genericType.MakeGenericType(typeArguments);
            var instance = Activator.CreateInstance(constructedType);
            return (instance as T);
        }
        #endregion

    }
}
