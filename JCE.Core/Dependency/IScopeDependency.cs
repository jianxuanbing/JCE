/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IScopeDependency
 * 版本号：v1.0.0.0
 * 唯一标识：1079f076-44d9-47af-b14f-cd61bad41456
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 15:30:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 15:30:50
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
    /// 依赖注入——局部模式，实现此接口的类型将被注册为<see cref="LifetimeStyle.Scoped"/>模式
    /// </summary>
    public interface IScopeDependency:IDependency
    {
    }
}
