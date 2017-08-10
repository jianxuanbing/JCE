using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// 枚举 操作
    /// </summary>
    public static class Enum
    {
        #region Parse(获取实例)
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名或值，范例：Enum1枚举有成员A=0，则传入"A"或"0"获取 Enum1.A</param>
        /// <returns></returns>
        public static TEnum Parse<TEnum>(object member)
        {
            string value = member.SafeString();
            if (value.IsEmpty())
            {
                throw new ArgumentNullException(nameof(member));
            }
            return (TEnum)System.Enum.Parse(Common.GetType<TEnum>(), value, true);
        }
        #endregion

        #region GetName(获取成员名)
        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可，范例：Enum1枚举有成员A=0，则传入Enum1.A或0，获取成员名"A"</param>
        /// <returns></returns>
        public static string GetName<TEnum>(object member)
        {
            return GetName(Common.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可，范例：Enum1枚举有成员A=0，则传入Enum1.A或0，获取成员名"A"</param>
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
            if (type.GetTypeInfo().IsEnum == false)
            {
                return string.Empty;
            }
            return System.Enum.GetName(type, member);
        }
        #endregion

        #region GetNames(获取枚举所有成员名称)
        /// <summary>
        /// 获取枚举所有成员名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns></returns>
        public static string[] GetNames<TEnum>()
        {
            return GetNames(typeof(TEnum));
        }

        /// <summary>
        /// 获取枚举所有成员名称
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static string[] GetNames(Type type)
        {
            return System.Enum.GetNames(type);
        }
        #endregion

        #region GetValue(获取成员值)
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可，范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static int GetValue<TEnum>(object member)
        {
            return GetValue(Common.GetType<TEnum>(), member);
        }
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可，范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static int GetValue(Type type, object member)
        {
            string value = member.SafeString();
            if (value.IsEmpty())
            {
                throw new ArgumentNullException(nameof(member));
            }
            return (int)System.Enum.Parse(type, member.ToString(), true);
        }
        #endregion

        #region GetDescription(获取描述)
        /// <summary>
        /// 获取描述，使用<see cref="Description"/>特性设置描述
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static string GetDescription<T>(object member)
        {
            return Reflection.GetDescription<T>(GetName<T>(member));
        }
        /// <summary>
        /// 获取描述，使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        /// <returns></returns>
        public static string GetDescription(Type type, object member)
        {
            return Reflection.GetDescription(type, GetName(type, member));
        }
        #endregion
    }
}
