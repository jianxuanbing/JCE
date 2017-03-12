/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServiceBuildOptions
 * 版本号：v1.0.0.0
 * 唯一标识：c37ee7c9-83f8-4d9f-b537-45a16dde00af
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:33:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:33:05
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
using JCE.Core.Reflection;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 服务创建配置信息
    /// </summary>
    public class ServiceBuildOptions
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个<see cref="ServiceBuildOptions"/>类型的新实例
        /// </summary>
        public ServiceBuildOptions()
        {
            AssemblyFinder=new DirectoryAssemblyFinder();
            TransientTypeFinder=new TransientDependencyTypeFinder();
            ScopeTypeFinder=new ScopeDependencyTypeFinder();
            SingletonTypeFinder=new SingletonDependencyTypeFinder();
        }
        #endregion

        #region Property(属性)
        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 获取或设置 即时生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder TransientTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 范围生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder ScopeTypeFinder { get; set; }
        
        /// <summary>
        /// 获取或设置 单例生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder SingletonTypeFinder { get; set; }
        #endregion
    }
}
