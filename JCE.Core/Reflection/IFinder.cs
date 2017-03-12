/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Reflection
 * 文件名：IFinder
 * 版本号：v1.0.0.0
 * 唯一标识：72bd5363-b01e-4c31-a949-20bdfac2a418
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 13:57:13
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 13:57:13
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

namespace JCE.Core.Reflection
{
    /// <summary>
    /// 定义一个查找器
    /// </summary>
    /// <typeparam name="TItem">要查找的项类型</typeparam>
    public interface IFinder<out TItem>
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        TItem[] Find(Func<TItem, bool> predicate);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        TItem[] FindAll();
    }
}
