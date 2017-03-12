/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Reflection
 * 文件名：IAllAssemblyFinder
 * 版本号：v1.0.0.0
 * 唯一标识：0c322b36-e0e4-4422-a8f1-fe5dbd457c5a
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:17:34
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:17:34
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
using JCE.Core.Dependency;

namespace JCE.Core.Reflection
{
    /// <summary>
    /// 定义所有程序集查找器
    /// </summary>
    public interface IAllAssemblyFinder:IAssemblyFinder,ISingletonDependency
    {
    }
}
