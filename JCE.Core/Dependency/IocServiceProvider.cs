/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：IocServiceProvider
 * 版本号：v1.0.0.0
 * 唯一标识：23d99aa7-61d2-424b-965c-2bb4fc29ae82
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 11:53:23
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 11:53:23
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
    /// 默认IOC服务提供者实现
    /// </summary>
    public class IocServiceProvider:IServiceProvider
    {
        private readonly IIocResolver _resolver;
        /// <summary>
        /// 初始化一个<see cref="IocServiceProvider"/>类型的新实例
        /// </summary>
        /// <param name="resolver">依赖注入对象解析获取器</param>
        public IocServiceProvider(IIocResolver resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// 获取指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">一个对象，它指定要获取的服务对象的类型</param>
        /// <filterproiority>2</filterproiority>
        /// <returns>
        /// <paramref name="serviceType"/>类型的服务对象，- 或 - 如果没有<paramref name="serviceType"/>类型的服务对象，则为null
        /// </returns>
        public object GetService(Type serviceType)
        {
            return _resolver.Resolve(serviceType);
        }
    }
}
