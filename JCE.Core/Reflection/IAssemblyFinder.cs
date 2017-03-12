/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Reflection
 * 文件名：IAssemblyFinder
 * 版本号：v1.0.0.0
 * 唯一标识：143071a6-b2b9-49ae-ad65-b1d3a5bf3070
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:16:53
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:16:53
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
    /// 定义程序集查找器
    /// </summary>
    public interface IAssemblyFinder:IFinder<Assembly>
    {
    }
}
