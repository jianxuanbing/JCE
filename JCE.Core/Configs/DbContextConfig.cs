/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：DbContextConfig
 * 版本号：v1.0.0.0
 * 唯一标识：1067c04b-4854-4cbc-8391-c404ec681c7f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:43:06
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:43:06
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
using JCE.Core.Configs.ConfigFile;
using JCE.Core.Properties;
using JCE.Utils.Extensions;

namespace JCE.Core.Configs
{
    /// <summary>
    /// 数据上下文配置
    /// </summary>
    public class DbContextConfig
    {
        /// <summary>
        /// 获取或设置 上下文名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否可用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据日志
        /// </summary>
        public bool DataLoggingEnabled { get; set; }

        /// <summary>
        /// 获取或设置 数据库连接串名称
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 获取或设置 数据上下文类型
        /// </summary>
        public Type ContextType { get; set; }

        /// <summary>
        /// 获取或设置 数据上下文初始化配置
        /// </summary>
        public DbContextInitializerConfig InitializerConfig { get; set; }

        /// <summary>
        /// 初始化一个<see cref="DbContextConfig"/>类型的新实例
        /// </summary>
        public DbContextConfig()
        {
            Name = Guid.NewGuid().ToString();
            Enabled = true;
            DataLoggingEnabled = false;
        }

        /// <summary>
        /// 初始化一个<see cref="DbContextConfig"/>类型的新实例
        /// </summary>
        internal DbContextConfig(ContextElement element)
        {
            Name = element.Name;
            Enabled = element.Enabled;
            DataLoggingEnabled = element.DataLoggingEnabled;
            ConnectionStringName = element.ConnectionStringName;
            ContextType = Type.GetType(element.ContextTypeName);
            if (ContextType == null)
            {
                throw new InvalidOperationException(Resources.ConfigFile_NameToTypeIsNull.FormatWith(element.ContextTypeName));
            }
            InitializerConfig = new DbContextInitializerConfig(element.DbContextInitializer);
        }
    }
}
