/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：ContextElement
 * 版本号：v1.0.0.0
 * 唯一标识：bc3de803-ff12-44dc-9d32-8829554fd743
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:47:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:47:35
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
    /// 数据上下文配置节点
    /// </summary>
    internal class ContextElement: ConfigurationElement
    {
        private const string NameKey = "name";
        private const string EnabledKey = "enabled";
        private const string DataLoggingEnabledKey = "dataLoggingEnabled";
        private const string ConnectionStringNameKey = "connectionStringName";
        private const string TypeKey = "type";
        private const string DbContextInitializerKey = "initializer";

        /// <summary>
        /// 获取或设置 节点名称
        /// </summary>
        [ConfigurationProperty(NameKey, IsRequired = true, IsKey = true)]
        public virtual string Name
        {
            get { return (string)this[NameKey]; }
            set { this[NameKey] = value; }
        }

        /// <summary>
        /// 获取或设置 是否启用数据上下文
        /// </summary>
        [ConfigurationProperty(EnabledKey, DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool)this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 是否开启数据日志记录
        /// </summary>
        [ConfigurationProperty(DataLoggingEnabledKey, DefaultValue = false)]
        public virtual bool DataLoggingEnabled
        {
            get { return (bool)this[DataLoggingEnabledKey]; }
            set { this[DataLoggingEnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据库连接串名称
        /// </summary>
        [ConfigurationProperty(ConnectionStringNameKey, IsRequired = true)]
        public virtual string ConnectionStringName
        {
            get { return (string)this[ConnectionStringNameKey]; }
            set { this[ConnectionStringNameKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据上下文类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string ContextTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据上下文初始化配置
        /// </summary>
        [ConfigurationProperty(DbContextInitializerKey, IsRequired = true)]
        public virtual DbContextInitializerElement DbContextInitializer
        {
            get { return (DbContextInitializerElement)this[DbContextInitializerKey]; }
            set { this[DbContextInitializerKey] = value; }
        }
    }
}
