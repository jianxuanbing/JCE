/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Reflection
 * 文件名：IMethodInfoFinder
 * 版本号：v1.0.0.0
 * 唯一标识：28bf1bc3-9143-456c-b7cb-06fe2cd08588
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：9/8 星期四 14:40:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：9/8 星期四 14:40:58
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

namespace JCE.Core.Reflection
{
    /// <summary>
    /// 定义方法信息查找器
    /// </summary>
    public interface IMethodInfoFinder
    {
        /// <summary>
        /// 查找指定条件的方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        MethodInfo[] Find(Type type, Func<MethodInfo, bool> predicate);

        /// <summary>
        /// 从指定类型查找方法信息
        /// </summary>
        /// <param name="type">控制器类型</param>
        /// <returns></returns>
        MethodInfo[] FindAll(Type type);
    }
}
