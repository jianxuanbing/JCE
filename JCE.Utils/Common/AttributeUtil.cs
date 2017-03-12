/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：AttributeUtil
 * 版本号：v1.0.0.0
 * 唯一标识：85055c55-29c4-4bd2-8831-7365196084a7
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:27:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:27:05
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Common
{
    /// <summary>
    /// 属性操作工具类
    /// </summary>
    public class AttributeUtil
    {
        #region GetAttribute(获取属性)
        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <typeparam name="TAttribute">泛型属性</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
        {
            return (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        }

        /// <summary>
        /// 获取属性信息数组
        /// </summary>
        /// <typeparam name="TAttribute">泛型属性</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static TAttribute[] GetAttributes<TAttribute>(MemberInfo memberInfo)
        {
            return Array.ConvertAll(memberInfo.GetCustomAttributes(typeof(TAttribute), false), x => (TAttribute)x);
        }
        #endregion
    }
}
