/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：LifetimeStyle
 * 版本号：v1.0.0.0
 * 唯一标识：2e73132f-4997-4d48-8903-89870a8c0983
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 15:28:56
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 15:28:56
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入的对象生命周期
    /// </summary>
    public enum LifetimeStyle
    {
        /// <summary>
        /// 实时模式，每次获取都创建不同对象
        /// </summary>
        Transient,
        /// <summary>
        /// 局部模式，同一生命周期获得相同对象，不同生命周期获得不同对象（PerRequest）
        /// </summary>
        Scoped,
        /// <summary>
        /// 单例模式，在第一次获取实例时创建，之后都获得相同对象
        /// </summary>
        Singleton
    }
}
