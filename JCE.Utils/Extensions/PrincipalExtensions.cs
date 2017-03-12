/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：PrincipalExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：f25f0909-bc23-46f9-ad1e-e1529a7af167
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:52:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:52:58
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 安全帐户（Principal）扩展
    /// </summary>
    public static class PrincipalExtensions
    {
        /// <summary>
        /// 获取属性值，从帐户安全的联系人数据中获取
        /// </summary>
        /// <param name="principal">帐户安全</param>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public static object GetProperty(this Principal principal, string name)
        {
            var directoryEntry = (principal.GetUnderlyingObject() as DirectoryEntry);
            if (directoryEntry != null && directoryEntry.Properties.Contains(name))
            {
                var property = directoryEntry.Properties[name];
                if (property != null)
                {
                    return property.Value;
                }
            }
            return null;
        }
    }
}
