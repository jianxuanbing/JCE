/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Autofac.LocalApp.Initialize
 * 文件名：AppBuilderExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：f5648bf7-b63a-4eea-bbbc-01e49cc87b19
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 13:50:15
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 13:50:15
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

using JCE.Core.Dependency;
using JCE.Core.Initialize;
using JCE.Utils.Extensions;
using Owin;

namespace JCE.Autofac.LocalApp.Initialize
{
    /// <summary>
    /// App构建器<see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化本定程序集框架
        /// </summary>
        /// <param name="app">App构建器</param>
        /// <param name="iocBuilder">依赖注入构建器</param>
        /// <returns></returns>
        public static IAppBuilder UseLocalInitialize(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer=new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}
