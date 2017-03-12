/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ComponentExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：820467f7-80ae-4b87-96b2-6e1afd8c2d69
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:40:19
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:40:19
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 组件（Component）扩展
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// 判断目标组件是否处于设计模式
        /// </summary>
        /// <param name="target">组件</param>
        /// <returns>bool</returns>
        public static bool IsInDesignMode(this IComponent target)
        {
            var site = target.Site;
            return !ReferenceEquals(site, null) && site.DesignMode;
        }
        /// <summary>
        /// 判断目标组件是否不处于设计模式
        /// </summary>
        /// <param name="target">组件</param>
        /// <returns>bool</returns>
        public static bool IsInRuntimeMode(this IComponent target)
        {
            return !target.IsInDesignMode();
        }
    }
}
