/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IIocBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：753e6140-809c-4d69-8dde-e044aa75cc83
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 16:03:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 16:03:47
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
    /// 定义依赖注入构建器，解析依赖注入服务映射信息进行构建
    /// </summary>
    public interface IIocBuilder
    {
        /// <summary>
        /// 获取服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 开始构建依赖注入映射
        /// </summary>
        /// <returns>服务提供者</returns>
        IServiceProvider Build();
    }
}
