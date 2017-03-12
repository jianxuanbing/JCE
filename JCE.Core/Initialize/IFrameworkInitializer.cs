/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Initialize
 * 文件名：IFrameworkInitializer
 * 版本号：v1.0.0.0
 * 唯一标识：8ce652b4-98cd-4fd0-a039-36f81da01981
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 14:01:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 14:01:43
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

namespace JCE.Core.Initialize
{
    /// <summary>
    /// 框架初始化接口
    /// </summary>
    public interface IFrameworkInitializer
    {
        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        void Initialize(IIocBuilder iocBuilder);
    }
}
