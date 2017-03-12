/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ITransientDependency
 * 版本号：v1.0.0.0
 * 唯一标识：dd6ab55f-9f3b-4949-a355-92f546bb72bf
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 15:34:13
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 15:34:13
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
    /// 依赖注入——实时模式，实现此接口的类型将被注册为<see cref="LifetimeStyle.Transient"/>模式
    /// </summary>
    public interface ITransientDependency:IDependency
    {
    }
}
