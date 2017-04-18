/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：EnumExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b66b61da-d947-46d4-a8fb-96f0e6733ff4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:44:55
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:44:55
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
using JCE.Utils.Common;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 枚举（Enum）扩展
    /// </summary>
    public static class EnumExtensions
    {        
        #region Value(获取成员值)
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static int Value(this Enum instance)
        {
            return EnumUtil.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static T Value<T>(this Enum instance)
        {
            return Conv.To<T>(Value(instance));
        }
        #endregion
        #region ToDescription(获取枚举特性的文字描述)
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>枚举特性文字说明</returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.ToDescription() : value.ToString();
        }
        #endregion
        #region ToDictionary(将枚举转换为字典)
        /// <summary>
        /// 将枚举类型转换为字典（键值对集合）
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>以枚举值为key，枚举文本为value的键值对集合</returns>
        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型!",nameof(enumType));
            }
            Dictionary<int,string> enumDic=new Dictionary<int, string>();
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                int key = Convert.ToInt32(enumValue);
                string value = enumValue.ToDescription();
                enumDic.Add(key,value);
            }
            return enumDic;
        }
        #endregion
        #region ClearFlag(删除标识符)
        /// <summary>
        /// 删除标识符并返回新值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flag">需要删除的标识符</param>
        /// <returns>返回新值</returns>
        public static T ClearFlag<T>(this Enum variable, T flag)
        {
            return variable.ClearFlags(flag);
        }
        /// <summary>
        /// 删除标识符数组并返回新值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flags">需要删除的标识符集合</param>
        /// <returns>返回新值</returns>
        public static T ClearFlags<T>(this Enum variable, params T[] flags)
        {
            var result = Convert.ToUInt64(variable);
            result = flags.Aggregate(result, (current, flag) => current & ~Convert.ToUInt64(flag));
            return (T)Enum.Parse(variable.GetType(), result.ToString());
        }
        #endregion
        #region SetFlag(设置标识符)
        /// <summary>
        /// 设置标识符并返回新值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flag">需要指定的标识符</param>
        /// <returns>返回新值</returns>
        public static T SetFlag<T>(this Enum variable, T flag)
        {
            return variable.SetFlags(flag);
        }
        /// <summary>
        /// 设置标识符集合并返回新值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flags">需要指定的标识符集合</param>
        /// <returns>返回新值</returns>
        public static T SetFlags<T>(this Enum variable, params T[] flags)
        {
            var result = Convert.ToUInt64(variable);
            result = flags.Aggregate(result, (current, flag) => current | Convert.ToUInt64(flag));
            return (T)Enum.Parse(variable.GetType(), result.ToString());
        }
        #endregion
        #region HasFlag(检查标识符)
        /// <summary>
        /// 检查标识符，判断枚举是否标识符
        /// </summary>
        /// <typeparam name="T">标识符类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flag">需要检查的标识符</param>
        /// <returns>bool</returns>
        public static bool HasFlag<T>(this T variable, T flag) where T : struct,IComparable, IFormattable, IConvertible
        {
            return variable.HasFlags(flag);
        }
        /// <summary>
        /// 检查标识符，判断枚举是否有特定的标识符集合
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="variable">枚举值</param>
        /// <param name="flags">需要检查的标识符集合</param>
        /// <returns>bool</returns>
        public static bool HasFlags<T>(this T variable, params T[] flags)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("variable must be an Enum", "variable");
            }
            foreach (var flag in flags)
            {
                if (!Enum.IsDefined(typeof(T), flag))
                {
                    return false;
                }
                ulong numFlag = Convert.ToUInt64(flag);
                if ((Convert.ToUInt64(variable) & numFlag) != numFlag)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
        #region ToInt(将枚举转换成Int类型)
        /// <summary>
        /// 将枚举转换成Int类型
        /// </summary>
        /// <param name="enumVal">枚举值</param>
        /// <returns></returns>
        public static int ToInt(this Enum enumVal)
        {
            return Convert.ToInt32(enumVal);
        }
        #endregion
    }
}
