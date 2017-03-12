/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ObjectFactory
 * 版本号：v1.0.0.0
 * 唯一标识：899ea218-8102-4ff5-ba9b-26300c6f8318
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 13:55:37
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 13:55:37
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
    /// 对象创建委托
    /// </summary>
    /// <param name="provider">服务提供者</param>
    /// <param name="args">构造函数的参数</param>
    /// <returns></returns>
    public delegate object ObjectFactory(IServiceProvider provider, object[] args);
}
