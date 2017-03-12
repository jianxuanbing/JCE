/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IServicesBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：7747bae3-60ec-4bb2-b56d-b05670f29b10
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 16:05:42
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 16:05:42
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
    /// 定义服务器映射集合创建功能
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// 构建当前服务，并添加到服务映射集合中
        /// </summary>
        /// <returns>服务映射集合</returns>
        IServiceCollection Build();
    }
}
