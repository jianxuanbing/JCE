/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：DbContextInitializerElement
 * 版本号：v1.0.0.0
 * 唯一标识：34b5f24f-4e89-4174-b51e-ea49d5c0c8be
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:48:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:48:47
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据上下文初始化配置节点
    /// </summary>
    internal class DbContextInitializerElement: ConfigurationElement
    {
        private const string TypeKey = "type";
        private const string EntityMapperFilesKey = "mapperFiles";
        private const string CreateDatabaseInitializerKey = "createInitializer";

        /// <summary>
        /// 获取或设置 初始化配置类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string InitializerTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }

        /// <summary>
        /// 获取或设置 实体映射类所在程序集名称字符串
        /// </summary>
        [ConfigurationProperty(EntityMapperFilesKey, IsRequired = true)]
        public virtual string EntityMapperFiles
        {
            get { return (string)this[EntityMapperFilesKey]; }
            set { this[EntityMapperFilesKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据库创建策略配置
        /// </summary>
        [ConfigurationProperty(CreateDatabaseInitializerKey)]
        public virtual CreateDatabaseInitializerElement CreateDatabaseInitializer
        {
            get { return (CreateDatabaseInitializerElement)this[CreateDatabaseInitializerKey]; }
            set { this[CreateDatabaseInitializerKey] = value; }
        }
    }
}
