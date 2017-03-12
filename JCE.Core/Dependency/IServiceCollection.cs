/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IServiceCollection
 * 版本号：v1.0.0.0
 * 唯一标识：a5ae4851-061e-480b-a7ef-bb8af99179ec
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/13 星期三 16:06:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/13 星期三 16:06:17
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
    /// 定义服务映射信息集合，用于装载类型映射的描述信息
    /// </summary>
    public interface IServiceCollection:IList<ServiceDescriptor>
    {
        /// <summary>
        /// 克隆创建当前集合的副本
        /// </summary>
        /// <returns></returns>
        IServiceCollection Clone();
    }
}
