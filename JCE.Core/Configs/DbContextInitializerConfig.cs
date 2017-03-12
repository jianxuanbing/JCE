/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：DbContextInitializerConfig
 * 版本号：v1.0.0.0
 * 唯一标识：859a1d22-9f6b-4ff4-bd11-430e76065447
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:51:29
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:51:29
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Configs.ConfigFile;
using JCE.Core.Properties;
using JCE.Utils.Extensions;

namespace JCE.Core.Configs
{
    /// <summary>
    /// 数据上下文初始化配置
    /// </summary>
    public class DbContextInitializerConfig
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerConfig"/>类型的新实例
        /// </summary>
        public DbContextInitializerConfig()
        {
            EntityMapperAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerConfig"/>类型的新实例
        /// </summary>
        internal DbContextInitializerConfig(DbContextInitializerElement element)
        {
            Type type = Type.GetType(element.InitializerTypeName);
            if (type == null)
            {
                throw new InvalidOperationException(Resources.DbContextInitializerConfig_InitializerNotExists.FormatWith(element.InitializerTypeName));
            }
            InitializerType = type;

            string binPath = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            string[] mapperFiles = element.EntityMapperFiles.Split(',')
                .Select(fileName => fileName.EndsWith(".dll") ? fileName : fileName + ".dll")
                .Select(fileName => Path.Combine(binPath, fileName)).ToArray();
            EntityMapperAssemblies = mapperFiles.Select(Assembly.LoadFrom).ToList();

            if (element.CreateDatabaseInitializer != null && !element.CreateDatabaseInitializer.InitializerTypeName.IsEmpty())
            {
                CreateDatabaseInitializerType = Type.GetType(element.CreateDatabaseInitializer.InitializerTypeName);
                if (CreateDatabaseInitializerType == null)
                {
                    throw new InvalidOperationException(Resources.ConfigFile_NameToTypeIsNull.FormatWith(element.CreateDatabaseInitializer.InitializerTypeName));
                }
            }
        }

        /// <summary>
        /// 获取或设置 数据上下文初始化类型
        /// </summary>
        public Type InitializerType { get; set; }

        /// <summary>
        /// 获取或设置 实体映射类型所在程序集集合
        /// </summary>
        public ICollection<Assembly> EntityMapperAssemblies { get; set; }

        /// <summary>
        /// 获取或设置 创建数据库初始化类型
        /// </summary>
        public Type CreateDatabaseInitializerType { get; set; }
    }
}
