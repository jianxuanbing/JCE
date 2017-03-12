/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：DataConfigReseter
 * 版本号：v1.0.0.0
 * 唯一标识：fd1688ed-505b-4c26-a496-75aa403ed02c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:56:41
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:56:41
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
using JCE.Core.Configs;
using JCE.Core.Reflection;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 数据配置信息重置类
    /// </summary>
    public class DataConfigReseter:IDataConfigReseter
    {
        /// <summary>
        /// 初始化一个<see cref="DataConfigReseter"/>类型的新实例
        /// </summary>
        public DataConfigReseter()
        {
            MapperAssemblyFinder = new EntityMapperAssemblyFinder()
            {
                AllAssemblyFinder = new DirectoryAssemblyFinder()
            };
        }

        /// <summary>
        /// 获取或设置 实体映射程序集查找器
        /// </summary>
        public IEntityMapperAssemblyFinder MapperAssemblyFinder { get; set; }

        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        public DataConfig Reset(DataConfig config)
        {
            //没有上下文，添加默认上下文
            if (!config.ContextConfigs.Any())
            {
                DbContextConfig contextConfig = GetDefaultDbContextConfig();
                config.ContextConfigs.Add(contextConfig);
            }
            //如果业务上下文存在开启数据日志功能，并且日志上下文没有设置，则添加日志上下文
            //if (config.ContextConfigs.All(m => m.ContextType != typeof(LoggingDbContext)))
            //{
            //    DbContextConfig contextConfig = GetLoggingDbContextConfig();
            //    config.ContextConfigs.Add(contextConfig);
            //}
            return config;
        }

        /// <summary>
        /// 获取默认业务上下文配置信息
        /// </summary>
        /// <returns></returns>
        protected virtual DbContextConfig GetDefaultDbContextConfig()
        {
            return new DbContextConfig()
            {
                ConnectionStringName = "default",
                //ContextType = typeof(DefaultDbContext),
                //InitializerConfig = new DbContextInitializerConfig()
                //{
                //    InitializerType = typeof(DefaultDbContextInitializer),
                //    EntityMapperAssemblies = MapperAssemblyFinder.FindAll()
                //}
            };
        }

        /// <summary>
        /// 获取默认日志上下文配置信息
        /// </summary>
        /// <returns></returns>
        protected virtual DbContextConfig GetLoggingDbContextConfig()
        {
            return new DbContextConfig()
            {
                ConnectionStringName = "default",
                //ContextType = typeof(LoggingDbContext),
                //InitializerConfig = new DbContextInitializerConfig()
                //{
                //    InitializerType = typeof(LoggingDbContextInitializer),
                //    EntityMapperAssemblies = { typeof(LoggingDbContext).Assembly }
                //}
            };
        }
    }
}
