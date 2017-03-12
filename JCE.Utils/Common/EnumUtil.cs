/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：EnumUtil
 * 版本号：v1.0.0.0
 * 唯一标识：d7caa0ab-c482-4253-9588-76b45daa5ce9
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:08:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:08:05
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
using JCE.Utils.Extensions;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public static class EnumUtil
    {
        #region GetInstance(获取实例)
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
        /// <returns></returns>
        public static T GetInstance<T>(object member)
        {
            string value = member.ToStr();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(member));
            }
            return (T)Enum.Parse(Sys.GetType<T>(), value, true);
        }

        #endregion

        #region GetName(获取成员名)
        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,则传入Enum1.A或0,获取成员名"A"</param>
        /// <returns></returns>
        public static string GetName<T>(object member)
        {
            return GetName(Sys.GetType<T>(), member);
        }
        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        /// <returns></returns>
        public static string GetName(Type type, object member)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (member == null)
            {
                return string.Empty;
            }
            if (member is string)
            {
                return member.ToString();
            }
            if (type.IsEnum == false)
            {
                return string.Empty;
            }
            return Enum.GetName(type, member);
        }

        #endregion

        #region GetNames(获取枚举所有成员名称)
        /// <summary>
        /// 获取枚举所有成员名称
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>
        public static string[] GetNames<T>()
        {
            return GetNames(typeof(T));
        }
        /// <summary>
        /// 获取枚举所有成员名称
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static string[] GetNames(Type type)
        {
            return Enum.GetNames(type);
        }
        #endregion

        #region GetValue(获取成员值)
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static int GetValue<T>(object member)
        {
            return GetValue(Sys.GetType<T>(), member);
        }
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static int GetValue(Type type, object member)
        {
            string value = member.ToStr();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(member));
            }
            return (int)Enum.Parse(type, member.ToString(), true);
        }
        #endregion

        #region GetDescription(获取描述)
        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static string GetDescription<T>(object member)
        {
            return Reflection.GetDescription<T>(GetName<T>(member));
        }
        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static string GetDescription(Type type, object member)
        {
            return Reflection.GetDescription(type, GetName(type, member));
        }
        #endregion

        #region GetItems(获取描述项集合)
        /// <summary>
        /// 获取描述项集合,文本设置为Description，值为Value
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns></returns>
        public static List<Item> GetItems<T>()
        {
            Type enumType = Sys.GetType<T>();
            ValidationIsEnum(enumType);
            var result = new List<Item>();
            foreach (var field in enumType.GetFields())
            {
                AddItem<T>(result, field, enumType);
            }
            return result.OrderBy(t => t.SortId).ToList();
        }
        /// <summary>
        /// 验证是否枚举类型
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        private static void ValidationIsEnum(Type enumType)
        {
            if (enumType.IsEnum == false)
            {
                throw new InvalidOperationException($"类型 {enumType} 不是枚举");
            }
        }
        /// <summary>
        /// 添加描述项
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="result">结果集</param>
        /// <param name="field">枚举字段</param>
        /// <param name="enumType">枚举类型</param>
        private static void AddItem<T>(ICollection<Item> result, FieldInfo field, Type enumType)
        {
            if (!field.FieldType.IsEnum)
            {
                return;
            }
            var value = GetValue<T>(field, enumType);
            var description = Reflection.GetDescription(field);
            var sortId = GetSortId(field);
            if (sortId == -1)
            {
                sortId = value;
            }
            result.Add(new Item(description, value.ToString(), sortId));
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="field">枚举字段</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        private static int GetValue<T>(FieldInfo field, Type enumType)
        {
            object memberValue = enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
            return GetValue<T>(memberValue);
        }
        /// <summary>
        /// 获取排序号
        /// </summary>
        /// <param name="field">枚举字段</param>
        /// <returns></returns>
        private static int GetSortId(FieldInfo field)
        {
            object attribute = field.GetCustomAttributes(typeof(OrderByAttribute), true).FirstOrDefault();
            if (attribute == null)
            {
                return -1;
            }
            return ((OrderByAttribute)attribute).SortId;
        }
        #endregion

        #region GetEnumItemByDescription(获取指定描述信息的枚举项)
        /// <summary>
        /// 获取指定描述信息的枚举项
        /// </summary>
        /// <typeparam name="TEnum">枚举类型。</typeparam>
        /// <param name="desc">枚举项描述信息。</param>
        /// <returns></returns>
        public static TEnum GetEnumItemByDescription<TEnum>(string desc)
        {
            if (desc.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(desc));
            }
            Type type = typeof(TEnum);
            FieldInfo[] finfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            FieldInfo info = finfos.FirstOrDefault(p => p.GetCustomAttribute<DescriptionAttribute>(false).Description == desc);
            if (info == null)
            {
                throw new Exception($"在枚举（{type.FullName}）中，未发现描述为“{desc}”的枚举项。");
            }
            return (TEnum) Enum.Parse(type, info.Name);
        }
        #endregion
    }
}
